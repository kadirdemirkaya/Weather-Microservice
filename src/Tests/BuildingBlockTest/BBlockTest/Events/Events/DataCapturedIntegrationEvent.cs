using BuildingBlock.Base.Models;

namespace BBlockTest.Events.Events
{
    public class DataCapturedIntegrationEvent : IntegrationEvent
    {
        public string str { get; set; }

        public DataCapturedIntegrationEvent(string str)
        {
            this.str = str;
        }
    }
}
