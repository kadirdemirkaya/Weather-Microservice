using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.MsSql
{
    public class MsSqlService<T, TId> : IMsSqlService<T, TId>
      where T : Entity<TId>
      where TId : ValueObject
    {
        private MsSqlConfig msSqlConfig;

        public MsSqlService(MsSqlConfig msSqlConfig)
        {
            this.msSqlConfig = msSqlConfig;
        }
    }

    public class MsSqlService<T> : IMsSqlService<T>
       where T : class
    {
        private MsSqlConfig msSqlConfig;

        public MsSqlService(MsSqlConfig msSqlConfig)
        {
            this.msSqlConfig = msSqlConfig;
        }
    }
}