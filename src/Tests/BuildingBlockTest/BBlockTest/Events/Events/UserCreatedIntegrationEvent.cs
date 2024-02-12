using BuildingBlock.Base.Models;

namespace BBlockTest.Events.Events
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public string Username { get; set; }

        public UserCreatedIntegrationEvent(string username)
        {
            Username = username;
        }
    }
}
