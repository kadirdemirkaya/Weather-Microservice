namespace BuildingBlock.Base.Extensions
{
    public static class KeyFormatterExtension
    {
        public static string Format(string name,double lat, double lon)
            => $"{name}_{lat}_{lon}";
    }
}
