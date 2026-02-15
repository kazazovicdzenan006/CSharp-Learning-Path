using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.CrossRoadDto
{
    public class CrossRoadReadDto
    {

        public int Id { get; set; }
        public string CityZone { get; set; }
        public string StreetName { get; set; }
        public string CrossName { get; set; }
        public double TrafficJamPercantage { get; set; }

    }
}
