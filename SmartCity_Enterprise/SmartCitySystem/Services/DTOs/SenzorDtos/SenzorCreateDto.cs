using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Services.DTOs.SenzorDtos
{
    public class SenzorCreateDto
    {
        public string Name { get; set; }
        public double Vrijednost { get; set; }
        public int GradId { get; set; }
    }
}
