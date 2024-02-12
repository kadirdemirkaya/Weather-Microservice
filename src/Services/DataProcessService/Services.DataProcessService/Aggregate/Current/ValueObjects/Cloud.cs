using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Current.ValueObjects
{
    public class Cloud : ValueObject
    {
        public int Aal { get; set; }

        public Cloud(int aal)
        {
            Aal = aal;
        }

        public static Cloud Create(int aal)
            => new(aal);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Aal;
        }
    }
}
