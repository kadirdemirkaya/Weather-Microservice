using BuildingBlock.Base.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BBlockTest.Aggregate
{
    public sealed class LocationId : ValueObject
    {
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();

        [BsonConstructor]
        public LocationId(ObjectId id)
        {
            Id = id;
        }

        public static LocationId CreateUnique()
        {
            return new(ObjectId.GenerateNewId());
        }

        public static LocationId Create(ObjectId Id)
        {
            return new LocationId(Id);
        }

        public static LocationId Create(string Id)
        {
            return new LocationId(ObjectId.Parse(Id));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
