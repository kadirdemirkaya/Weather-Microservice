namespace BuildingBlock.Base.Options
{
    public class JwtOptions
    {
        public string? Secret { get; set; }
        public string? ExpiryMinutes { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
