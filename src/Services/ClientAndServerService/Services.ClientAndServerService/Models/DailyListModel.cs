namespace Services.ClientAndServerService.Models
{
    public class DailyListModel
    {
        public int Dt { get; set; }
        public Models.Daily.Main Main { get; set; }
        public Models.Daily.Cloud Cloud { get; set; }
        public Models.Daily.Rain Rain { get; set; }

        public List<DWeatherModel> DWeatherModels { get; set; }
    }
}
