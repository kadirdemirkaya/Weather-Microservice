namespace Services.ClientAndServerService.Constants
{
    public static class Constant
    {
        public static class Application
        {
            public const string Name = "ClientAndServerService";
            public const string Version = "v1";
            public const string Description = "Client and server service api description";
        }
        public static class FilePaths
        {
            private static string currentDirectory = Directory.GetCurrentDirectory();
            private static string logsPath = Path.Combine(currentDirectory, "Logs".TrimStart('\\', '/'));
            private static IEnumerable<string> txtFiles = Directory.EnumerateFiles(logsPath, "*.txt");

            public static List<string> txtLogFiles = txtFiles.ToList();
        }

        public static class ClientAndServerService
        {
            public const string Tag = "ClientAndServer";
            public const string ID = "ClientAndServerService";
            public const string Name = "ClientAndServerService";
            public const string Host = "localhost";
            public const string Port = "7282";
        }
    }
}
