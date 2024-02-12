using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public class Cloud : ValueObject
    {
        public int All { get; set; }

        public Cloud(int all)
        {
            All = all;
        }

        public static Cloud Create(int all)
            => new(all);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return All;
        }
    }
}
