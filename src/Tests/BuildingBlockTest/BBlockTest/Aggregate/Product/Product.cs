using BuildingBlock.Base.Models.Base;

namespace BBlockTest.Aggregate.Product
{
    public class Product : AggregateRoot<ProductId>
    {
        public string ProductName { get; set; }
    }
}
