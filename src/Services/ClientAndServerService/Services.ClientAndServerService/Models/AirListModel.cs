namespace Services.ClientAndServerService.Models
{
    public class AirListModel
    {
        public int Dt { get; set; }
        public Models.Air.Main Main { get; set; }
        public Models.Air.Component Component { get; set; }
    }
}
