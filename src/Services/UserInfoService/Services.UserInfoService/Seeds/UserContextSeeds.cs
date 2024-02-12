using BuildingBlock.Base.Extensions;
using Microsoft.Data.SqlClient;
using Polly;
using Polly.Retry;
using Serilog;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.Enums;
using Services.UserInfoService.Aggregates.ValueObjects;
using Services.UserInfoService.Configurations.Configs;
using Services.UserInfoService.Constants;
using Services.UserInfoService.Data;

namespace Services.UserInfoService.Seeds
{
    public class UserContextSeeds
    {
        public async Task SeedAsync(UserDbContext context)
        {
            var policy = CreatePolicy(nameof(UserDbContext));

            await policy.ExecuteAsync(async () =>
            {
                try
                {
                    if (!context.Roles.Any() && !context.Users.Any())
                    {
                        await context.Roles.AddRangeAsync(GetSeedRolesDatas());
                        await context.SaveChangesAsync();

                        await context.Users.AddRangeAsync(GetSeedUsersDatas());
                        await context.SaveChangesAsync();
                    }
                    Log.Information("Seed Work is Succesfully");
                }
                catch (Exception ex)
                {
                    Log.Error("Seed Process Not Succesfully : " + ex.Message);
                    throw new Exception("Seed Process Not Succesfully : " + ex.Message);
                }

            });
        }
        private Role[] GetSeedRolesDatas()
        {
            return new[]
            {
                 Role.Create(Constant.Roles.Guest,RoleEnum.Guest, true),
                 Role.Create(Constant.Roles.User,RoleEnum.User, true),
                 Role.Create(Constant.Roles.Moderator,RoleEnum.Moderator, true),
                 Role.Create(Constant.Roles.Admin,RoleEnum.Admin, true),
                 Role.Create(Constant.Roles.Member,RoleEnum.Member, true),
            };
        }

        private User[] GetSeedUsersDatas()
        {
            User User1 = User.Create(Constant.Users.User1, "alex.34@gmail.com", PasswordHashExtension.StringHashingEncrypt("alex123", GetConfigs.GetEncryptionKey()), DateTime.Now, UserStatus.Active);
            User1.AddUserRole(RoleId.Create(Constant.Roles.User), UserId.Create(Constant.Users.User1), true, UserRoleStatus.Active);

            User User2 = User.Create(Constant.Users.User2, "alex.34@gmail.com", PasswordHashExtension.StringHashingEncrypt("alex123", GetConfigs.GetEncryptionKey()), DateTime.Now, UserStatus.Active);
            User2.AddUserRole(RoleId.Create(Constant.Roles.User), UserId.Create(Constant.Users.User2), true, UserRoleStatus.Active);

            User User3 = User.Create(Constant.Users.User3, "alex.34@gmail.com", PasswordHashExtension.StringHashingEncrypt("alex123", GetConfigs.GetEncryptionKey()), DateTime.Now, UserStatus.Active);
            User3.AddUserRole(RoleId.Create(Constant.Roles.User), UserId.Create(Constant.Users.User3), true, UserRoleStatus.Active);

            return new[]
            {
                User1
            };
        }

        private AsyncRetryPolicy CreatePolicy(string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        Log.Warning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
