using Library.Common.Commands;
using Library.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Library.Common.RabbitMq;

internal sealed class ScopedMessageDispatcher(IServiceScopeFactory scopeFactory)
{
    public async Task HandleCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>().HandleAsync(command);
    }

    public async Task HandleEventAsync<TEvent>(TEvent message) where TEvent : IEvent
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>().HandleAsync(message);
    }
}
