using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.UserInfoService.Configurations.Configs;
using Services.UserInfoService.Data;

namespace Services.UserInfoService
{
    public class DesignTimeContext : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<UserDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UserInfoDbContext;Trusted_Connection=True;");
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
