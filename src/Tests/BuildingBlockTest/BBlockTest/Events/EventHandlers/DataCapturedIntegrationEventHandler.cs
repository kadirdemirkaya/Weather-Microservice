using BBlockTest.Events.Events;
using BuildingBlock.Base.Abstractions;

namespace BBlockTest.Events.EventHandlers
{
    public class DataCapturedIntegrationEventHandler : IIntegrationEventHandler<DataCapturedIntegrationEvent>
    {
        public async Task Handle(DataCapturedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
