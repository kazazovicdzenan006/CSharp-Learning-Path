using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Worker : Person
    {
        public Worker() { }
        public Worker(int id, string name, string position, double experience)
        {
            Id = id;
            Name = name;
            this.Position = position;
            this.ExperienceInYears = experience;

        }

        public string Position { get; set; }
        public double? ExperienceInYears { get; set; }
 
        public (string position,  double? experience) WorkerData()
        {
            return (Position, ExperienceInYears);
        }

    }
}
