using Library.Common.Commands;
using Library.Common.Events;
using Library.Common.RabbitMq;
using Library.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Learning.Tests;

public class MessagingTests
{
    [Fact]
    public async Task SubscriptionStartupWaitsForReadiness()
    {
        var ready = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var service = new RabbitMqSubscriptionService(() => ready.Task);
        var startup = service.StartAsync(CancellationToken.None);
        Assert.False(startup.IsCompleted);
        ready.SetResult();
        await startup.WaitAsync(TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task SubscriptionFailuresPropagateToHostStartup()
    {
        var error = new InvalidOperationException("Broker unavailable");
        var service = new RabbitMqSubscriptionService(() => Task.FromException(error));
        Assert.Same(error, await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.StartAsync(CancellationToken.None)));
    }

    [Fact]
    public async Task SubscriptionStartupCanBeCancelledWhileWaiting()
    {
        var ready = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var service = new RabbitMqSubscriptionService(() => ready.Task);
        using var cancellation = new CancellationTokenSource();
        var startup = service.StartAsync(cancellation.Token);
        cancellation.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => startup);
        ready.SetResult();
    }

    [Fact]
    public async Task AlreadyCancelledStartupDoesNotSubscribe()
    {
        var subscribed = false;
        var service = new RabbitMqSubscriptionService(() =>
        {
            subscribed = true;
            return Task.CompletedTask;
        });
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        await Assert.ThrowsAsync<OperationCanceledException>(() => service.StartAsync(cancellation.Token));
        Assert.False(subscribed);
    }

    [Fact]
    public async Task ConcurrentMessagesGetSeparateScopesDisposedAfterHandling()
    {
        var probe = new HandlerProbe();
        await using var provider = BuildProvider(probe);
        var dispatcher = new ScopedMessageDispatcher(provider.GetRequiredService<IServiceScopeFactory>());
        var command = dispatcher.HandleCommandAsync(new TestCommand());
        var message = dispatcher.HandleEventAsync(new TestEvent());
        try
        {
            await probe.BothEntered.Task.WaitAsync(TimeSpan.FromSeconds(5));
            Assert.Equal(2, probe.Created);
            Assert.Equal(0, probe.Disposed);
        }
        finally
        {
            probe.Release.TrySetResult();
            await Task.WhenAll(command, message);
        }
        Assert.Equal(2, probe.Disposed);
    }

    [Fact]
    public async Task FailedMessageStillAsynchronouslyDisposesItsScope()
    {
        var probe = new HandlerProbe();
        probe.Release.SetResult();
        await using var provider = BuildProvider(probe);
        var dispatcher = new ScopedMessageDispatcher(provider.GetRequiredService<IServiceScopeFactory>());
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            dispatcher.HandleCommandAsync(new TestCommand(Fail: true)));
        Assert.Equal(1, probe.Disposed);
    }

    [Fact]
    public async Task MissingCommandHandlerFailsStartupBeforeConnectingToTheBroker()
    {
        var services = new ServiceCollection();
        services.AddCommandSubscription<TestCommand>();
        await using var provider = services.BuildServiceProvider(validateScopes: true);
        var subscription = provider.GetRequiredService<IHostedService>();
        var error = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            subscription.StartAsync(CancellationToken.None));
        Assert.Contains(nameof(ICommandHandler<TestCommand>), error.Message);
    }

    [Fact]
    public async Task MissingEventHandlerFailsStartupBeforeConnectingToTheBroker()
    {
        var services = new ServiceCollection();
        services.AddEventSubscription<TestEvent>();
        await using var provider = services.BuildServiceProvider(validateScopes: true);
        var subscription = provider.GetRequiredService<IHostedService>();
        var error = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            subscription.StartAsync(CancellationToken.None));
        Assert.Contains(nameof(IEventHandler<TestEvent>), error.Message);
    }

    [Fact]
    public async Task ServiceHostWaitsForShutdownAndStopsHostedServices()
    {
        var probe = new HostProbe();
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddSingleton<IHostedService>(probe);
        var serviceHost = new ServiceHost(builder.Build());
        using var cancellation = new CancellationTokenSource();
        var running = serviceHost.RunAsync(cancellation.Token);
        try
        {
            await probe.Started.Task.WaitAsync(TimeSpan.FromSeconds(5));
            Assert.False(running.IsCompleted);
        }
        finally
        {
            cancellation.Cancel();
            await running.WaitAsync(TimeSpan.FromSeconds(5));
        }
        Assert.True(probe.Stopped);
    }

    private static ServiceProvider BuildProvider(HandlerProbe probe)
    {
        var services = new ServiceCollection();
        services.AddSingleton(probe);
        services.AddScoped<ICommandHandler<TestCommand>, TestHandler>();
        services.AddScoped<IEventHandler<TestEvent>, TestHandler>();
        return services.BuildServiceProvider(validateScopes: true);
    }

    public sealed record TestCommand(bool Fail = false) : ICommand;
    public sealed record TestEvent : IEvent;

    public sealed class HandlerProbe
    {
        public int Created;
        public int Entered;
        public int Disposed;
        public TaskCompletionSource BothEntered { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public TaskCompletionSource Release { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    public sealed class TestHandler : ICommandHandler<TestCommand>, IEventHandler<TestEvent>, IAsyncDisposable
    {
        private readonly HandlerProbe probe;

        public TestHandler(HandlerProbe probe)
        {
            this.probe = probe;
            Interlocked.Increment(ref probe.Created);
        }

        public Task HandleAsync(TestCommand command) => HandleAsync(command.Fail);
        public Task HandleAsync(TestEvent message) => HandleAsync(fail: false);

        private async Task HandleAsync(bool fail)
        {
            if (Interlocked.Increment(ref probe.Entered) == 2)
            {
                probe.BothEntered.TrySetResult();
            }
            await probe.Release.Task;
            if (fail) throw new InvalidOperationException("Handler failed");
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Yield();
            Interlocked.Increment(ref probe.Disposed);
        }
    }

    private sealed class HostProbe : IHostedService
    {
        public TaskCompletionSource Started { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public bool Stopped { get; private set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Started.SetResult();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Stopped = true;
            return Task.CompletedTask;
        }
    }
}
