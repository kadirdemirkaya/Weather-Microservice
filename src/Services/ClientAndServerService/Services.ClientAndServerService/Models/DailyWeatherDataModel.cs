﻿using Services.ClientAndServerService.Models.Daily;

namespace Services.ClientAndServerService.Models
{
    public class DailyWeatherDataModel
    {
        public City City { get; set; }
        public List<DailyListModel> DailyListModel { get; set; }
    }
}
