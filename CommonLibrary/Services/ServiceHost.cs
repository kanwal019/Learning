using Library.Common.Commands;
using Library.Common.Events;
using Library.Common.RabbitMq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Common.Services;

// Compatibility adapter for consumers of the original fluent Startup-based API.
// Applications in this solution use WebApplication directly.
public class ServiceHost(IHost host) : IServiceHost
{
    public void Run() => host.Run();

    public Task RunAsync(CancellationToken cancellationToken = default) => host.RunAsync(cancellationToken);

    public static HostBuilder Create<TStartup>(string[] args) where TStartup : class =>
        new(Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(web => web.UseStartup<TStartup>()));

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public sealed class HostBuilder(IHostBuilder hostBuilder) : BuilderBase
    {
        public BusBuilder UseRabbitMq() => new(hostBuilder);

        public override ServiceHost Build() => new(hostBuilder.Build());
    }

    public sealed class BusBuilder(IHostBuilder hostBuilder) : BuilderBase
    {
        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            hostBuilder.ConfigureServices(services => services.AddCommandSubscription<TCommand>());
            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            hostBuilder.ConfigureServices(services => services.AddEventSubscription<TEvent>());
            return this;
        }

        public override ServiceHost Build() => new(hostBuilder.Build());
    }
}
