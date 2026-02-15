using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.CityNodeDto
{
    public class CityNodeReadDto
    {

        public int Id { get; set; }

        public string CityZone { get; set; }

        public string StreetName { get; set; }

        public int GradId { get; set; }
        public string GradName { get; set; }

    }
}
