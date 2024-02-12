using BuildingBlock.Base.Models.Base;
using System.Runtime.CompilerServices;

namespace BBlockTest.Aggregate
{
    public class Location : AggregateRoot<LocationId>
    {
        public string testStr { get; set; } = string.Empty;

    }
}
