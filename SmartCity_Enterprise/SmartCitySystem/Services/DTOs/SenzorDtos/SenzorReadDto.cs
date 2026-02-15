using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Services.DTOs.SenzorDtos
{
    public class SenzorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Vrijednost { get; set; } 
        public int GradId { get; set; }
        public string GradName { get; set; }

    }
}
