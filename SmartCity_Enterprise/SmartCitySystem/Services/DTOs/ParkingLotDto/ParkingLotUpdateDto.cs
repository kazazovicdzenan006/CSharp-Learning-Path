using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.ParkingLotDto
{
    public class ParkingLotUpdateDto
    {

        public string CityZone { get; set; }
        public string StreetName { get; set; }
        public string ParkingName { get; set; }
        public int TotalParkingSpots { get; set; }
        public int AvailableParkingSpots { get; set; }

    }
}
