using BuildingBlock.Base.Abstractions;

namespace BuildingBlock.Base.Concretes
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
