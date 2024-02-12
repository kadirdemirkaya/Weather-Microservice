namespace BuildingBlock.Base.Models.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string StatusPhrase { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public DateTime TimeSpan { get; set; }
    }
}
