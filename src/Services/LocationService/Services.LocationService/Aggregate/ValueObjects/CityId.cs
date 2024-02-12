using BuildingBlock.Base.Models.Base;
using MongoDB.Bson;

namespace Services.LocationService.Aggregate.ValueObjects
{
    public sealed class CityId : ValueObject
    {
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();

        public CityId(ObjectId id)
        {
            Id = id;
        }

        public static CityId CreateUnique()
        {
            return new(ObjectId.GenerateNewId());
        }

        public static CityId Create(ObjectId Id)
        {
            return new CityId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
