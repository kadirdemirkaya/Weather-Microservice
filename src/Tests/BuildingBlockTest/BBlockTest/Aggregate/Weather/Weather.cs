using BuildingBlock.Base.Models.Base;

namespace BBlockTest.Aggregate.Weather
{
    public class Weather : AggregateRoot<WeatherId>
    {
        public int Degree { get; set; }
    }
}
