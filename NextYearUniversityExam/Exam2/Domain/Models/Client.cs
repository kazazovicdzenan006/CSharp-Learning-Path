using Domain.Delegates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Models
{
    public class Client : Person {
        public Client() { }
        public Client(int id, string name, string company, string email)
        {
            Id = id;
            Name = name;
            this.CompanyName = company;
            this.Email = email;
        }

        public string CompanyName { get; set; }
        public string? Email { get; set; }
        
        [NotMapped]
        public Dictionary<int, Client> Clients { get; set; }

        public string Format() =>  $"ID: {Id}, Name: {Name}, Company name: {CompanyName}, Email: {Email}";




       

    }

}
