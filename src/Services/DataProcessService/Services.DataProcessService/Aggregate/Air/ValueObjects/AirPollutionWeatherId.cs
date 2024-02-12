using BuildingBlock.Base.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataProcessService.Aggregate.Air.ValueObjects
{
    public sealed class AirPollutionWeatherId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public AirPollutionWeatherId(Guid id)
        {
            Id = id;
        }

        public static AirPollutionWeatherId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static AirPollutionWeatherId Create(Guid Id)
        {
            return new AirPollutionWeatherId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
