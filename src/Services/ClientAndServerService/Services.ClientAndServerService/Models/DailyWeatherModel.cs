namespace Services.ClientAndServerService.Models
{
    public class DailyWeatherModel
    {
        public Models.Daily.City City { get; set; }

        public List<DListModel> DListModels { get; set; }

    }
}
