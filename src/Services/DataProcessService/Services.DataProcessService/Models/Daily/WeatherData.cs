namespace Services.DataProcessService.Models.Daily
{
    public class WeatherData
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<Models.Daily.List> list { get; set; }
        public City city { get; set; }
    }
}
