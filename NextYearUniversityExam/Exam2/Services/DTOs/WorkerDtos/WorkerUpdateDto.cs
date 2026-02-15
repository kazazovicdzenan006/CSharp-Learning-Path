using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTOs.WorkerDtos
{
   public class WorkerUpdateDto
    {
        public string Name { get; set; }
        
        public string Position { get; set; }
        public double? ExperienceInYears { get; set; }


    }
}
