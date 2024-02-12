namespace BuildingBlock.Base.Abstractions
{
    public interface IPubEventService
    {
        Task PublishDomainEventAsync(string serviceName);
    }
}
