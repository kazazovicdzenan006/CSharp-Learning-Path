using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.WorkerDtos
{
    public class WorkerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double? ExperienceInYears { get; set; }


    }
}
