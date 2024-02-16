namespace ApiGateway.Api.Constants
{
    public static class Constant
    {
        public static class Application
        {
            public const string Name = "ApiGateway";
            public const string Version = "v1";
            public const string Description = "Api Gateway description";
        }
        public static class FilePaths
        {
            private static string currentDirectory = Directory.GetCurrentDirectory();
            private static string logsPath = Path.Combine(currentDirectory, "Logs".TrimStart('\\', '/'));
            private static IEnumerable<string> txtFiles = Directory.EnumerateFiles(logsPath, "*.txt");

            public static List<string> txtLogFiles = txtFiles.ToList();
        }
    }
}
