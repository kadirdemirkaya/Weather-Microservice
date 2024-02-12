namespace BuildingBlock.Base.Models.Base
{
    public abstract class Enumeration<T> : IEquatable<Enumeration<T>>, IComparable<Enumeration<T>> where T : Enumeration<T>
    {
        public Guid Id { get; protected init; }

        public Enumeration()
        {

        }

        public Enumeration(Guid id, string name)
        {
            Id = id;
        }

        public int CompareTo(Enumeration<T>? other)
        {
            return other is null ? 1 : Id.CompareTo(other.Id);
        }

        public bool Equals(Enumeration<T>? other)
        {
            if (other is null)
                return false;
            return GetType() == other.GetType() && other.Id.Equals(Id);
        }
    }
}
