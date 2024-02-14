namespace Services.DataProcessService.Models.Daily
{
    public class DListModel
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public Cloud Cloud { get; set; }
        public Rain Rain { get; set; }

        public List<DWeatherModel> DWeatherModels { get; set; }
    }
}
