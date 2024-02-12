using BuildingBlock.Base.Abstractions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BuildingBlock.Base.Models.Base
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvent
         where TId : notnull
    {
        public TId Id { get; set; }
        //public TId Id { get; protected set; } ?????
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;


        protected List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();


        public Entity()
        {

        }
        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> Entity && Id.Equals(Entity.Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
