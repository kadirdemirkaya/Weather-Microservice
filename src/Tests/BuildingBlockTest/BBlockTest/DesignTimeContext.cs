using BBlockTest.Constants;
using BBlockTest.Models.Data.Product;
using BBlockTest.Models.Data.Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BBlockTest
{
    #region sql design time
    //public class DesignTimeContext : IDesignTimeDbContextFactory<ProductDbContext>
    //{
    //    public ProductDbContext CreateDbContext(string[] args)
    //    {
    //        DbContextOptionsBuilder<ProductDbContext> dbContextOptionsBuilder = new();
    //        dbContextOptionsBuilder.UseSqlServer(Constant.Sql.SqlDbUrl);
    //        return new(dbContextOptionsBuilder.Options);
    //    }
    //}
    #endregion

    public class DesignTimeContext2 : IDesignTimeDbContextFactory<WeatherDbContext>
    {
        public WeatherDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<WeatherDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Constant.Postgre.PostgreDbUrl);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
