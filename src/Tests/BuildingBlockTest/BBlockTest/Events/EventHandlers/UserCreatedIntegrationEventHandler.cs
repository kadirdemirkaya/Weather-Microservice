using BBlockTest.Events.Events;
using BuildingBlock.Base.Abstractions;

namespace BBlockTest.Events.EventHandlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public Task Handle(UserCreatedIntegrationEvent @event)
        {
            Console.WriteLine(@event.Username + @event.CreatedDate);
            return Task.CompletedTask;
        }
    }
}
