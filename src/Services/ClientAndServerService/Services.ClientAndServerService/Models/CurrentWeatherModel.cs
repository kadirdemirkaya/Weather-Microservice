namespace Services.ClientAndServerService.Models
{
    public class CurrentWeatherModel
    {
        public string Base { get; set; }
        public int Dt { get; set; }
        public Models.Current.Rain Rain { get; set; }
        public Models.Current.Cloud Cloud { get; set; }
        public Models.Current.Sys Sys { get; set; }

        public List<Models.Current.Weather> Weathers { get; set; }
    }
}
