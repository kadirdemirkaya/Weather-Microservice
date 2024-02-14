using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Models
{
    public class CurrentWeatherModel
    {
        public string @base { get; set; }
        public int dt { get; set; }
        public Rain Rain { get; set; }
        public Cloud Cloud { get; set; }
        public Sys Sys { get; set; }
        public List<Weather> Weathers { get; set; }
    }
}
