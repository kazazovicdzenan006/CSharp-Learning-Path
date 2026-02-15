using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTOs.WorkerDtos
{
    public class WorkerCreateDto
    {
       
        [Required(ErrorMessage = "Field Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field Position is required")]
        public string Position { get; set; }
        public double? ExperienceInYears { get; set; }



    }
}
