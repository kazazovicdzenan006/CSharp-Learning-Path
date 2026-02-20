using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.CrossRoadDto
{
    public class CrossRoadCreateDto
    {
        public string CityZone { get; set; }
        public string StreetName { get; set; }
        public string CrossName { get; set; }
        public double TrafficJamPercantage { get; set; }
        public int GradId { get; set; }

    }
}
