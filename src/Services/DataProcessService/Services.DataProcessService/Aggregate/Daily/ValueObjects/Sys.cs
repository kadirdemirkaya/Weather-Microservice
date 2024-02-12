using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public class Sys : ValueObject
    {
        public string Pod { get; set; }

        public Sys(string pod)
        {
            Pod = pod;
        }

        public static Sys Create(string pod)
            => new(pod);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Pod;
        }
    }
}
