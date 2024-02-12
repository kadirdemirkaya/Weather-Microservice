namespace Services.DataProcessService.Models.Daily
{
    //DAILY
    public class List
    {
        public int dt { get; set; }
        public Models.Daily.Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Cloud clouds { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public Rain rain { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
    }
}
