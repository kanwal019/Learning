using Library.Common.Commands;
using Library.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using System.Reflection;
using System.Threading.Tasks;

namespace Library.Common.RabbitMq;

public static class Extensions
{
    public static IServiceCollection AddCommandSubscription<TCommand>(this IServiceCollection services)
        where TCommand : ICommand
    {
        services.AddSingleton<IHostedService>(provider => new RabbitMqSubscriptionService(async () =>
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            await ValidateHandlerAsync<ICommandHandler<TCommand>>(scopeFactory);
            var dispatcher = new ScopedMessageDispatcher(scopeFactory);
            await provider.GetRequiredService<IBusClient>().SubscribeAsync<TCommand>(
                message => dispatcher.HandleCommandAsync(message),
                ctx => ctx.UseConsumerConfiguration(cfg => cfg.FromDeclaredQueue(
                    queue => queue.WithName(GetQueueName<TCommand>()))));
        }));
        return services;
    }

    public static IServiceCollection AddEventSubscription<TEvent>(this IServiceCollection services)
        where TEvent : IEvent
    {
        services.AddSingleton<IHostedService>(provider => new RabbitMqSubscriptionService(async () =>
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            await ValidateHandlerAsync<IEventHandler<TEvent>>(scopeFactory);
            var dispatcher = new ScopedMessageDispatcher(scopeFactory);
            await provider.GetRequiredService<IBusClient>().SubscribeAsync<TEvent>(
                message => dispatcher.HandleEventAsync(message),
                ctx => ctx.UseConsumerConfiguration(cfg => cfg.FromDeclaredQueue(
                    queue => queue.WithName(GetQueueName<TEvent>()))));
        }));
        return services;
    }

    private static async Task ValidateHandlerAsync<THandler>(IServiceScopeFactory scopeFactory)
        where THandler : notnull
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        _ = scope.ServiceProvider.GetRequiredService<THandler>();
    }

    public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
        ICommandHandler<TCommand> handler) where TCommand : ICommand
        => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
            ctx => ctx.UseConsumerConfiguration(cfg =>
            cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

    public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
        IEventHandler<TEvent> handler) where TEvent : IEvent
        => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
            ctx => ctx.UseConsumerConfiguration(cfg =>
            cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

    private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        var section = configuration.GetSection("RabbitMq");
        section.Bind(options);
        services.AddSingleton<IBusClient>(_ => RawRabbitFactory.CreateSingleton(
            new RawRabbitOptions { ClientConfiguration = options }));
    }
}

