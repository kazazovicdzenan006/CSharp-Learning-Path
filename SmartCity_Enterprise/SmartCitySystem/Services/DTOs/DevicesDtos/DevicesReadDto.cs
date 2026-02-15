using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.DevicesDtos
{
    public class DevicesReadDto
    {

        // we can only have ReadDto because class Uredjaji is Abstract class so we can't create objects of class Uredaji
        public int Id { get; set; }
        public string Name { get; set; }
        public int GradId { get; set; }
        public string GradName { get; set; }


    }
}
