using MongoDB.Bson.Serialization.Conventions;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.Enums;

namespace Services.UserInfoService.Constants
{
    public static class Constant
    {
        public static class Application
        {
            public const string Name = "UserInfoService";
            public const string Version = "v1";
            public const string Description = "User info service api description";
        }
        public static class FilePaths
        {
            private static string currentDirectory = Directory.GetCurrentDirectory();
            private static string logsPath = Path.Combine(currentDirectory, "Logs".TrimStart('\\', '/'));
            private static IEnumerable<string> txtFiles = Directory.EnumerateFiles(logsPath, "*.txt");

            public static List<string> txtLogFiles = txtFiles.ToList();
        }

        public static class Tables
        {
            public const string Users = $"{nameof(User)}s";
            public const string Roles = $"{nameof(Role)}s";
            public const string RoleUsers = $"{nameof(RoleUser)}s";
        }

        public static class Users
        {
            public static Guid User1 = Guid.Parse("37f1446b-cb13-47eb-b067-9ac654dddc3a");
            public static Guid User2 = Guid.Parse("60d23a56-a7b5-40b1-8d3c-892cad64f926");
            public static Guid User3 = Guid.Parse("1bb31d1b-649a-4fab-92a1-361cd8a5d916");
        }

        public static class Roles
        {
            public static Guid Admin = Guid.Parse("803e0ac3-5363-4089-bbd6-357079575d5b");
            public static Guid Guest = Guid.Parse("71d757fe-7fdf-4a5c-9cd0-2ccf006a32cd");
            public static Guid Member = Guid.Parse("8b9ff64e-bc2a-4334-9fa0-d8ee551926a9");
            public static Guid Moderator = Guid.Parse("0592d157-85c6-4bdd-b3f7-3daec55d7839");
            public static Guid User = Guid.Parse("d222cc2d-2e26-44ab-acb1-e67165a81361");
        }

        public static class Keys
        {
            public const string UserEmail = "User_Email";
            public const string UserToken = "User_Token";
            public const string UserRole = "User_Role";
        }
    }
}
