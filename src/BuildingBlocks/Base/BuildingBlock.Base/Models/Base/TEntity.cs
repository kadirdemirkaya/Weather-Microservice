using MongoDB.Bson;

namespace BuildingBlock.Base.Models.Base
{
    public class TEntity
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}
