using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTOs.ClientDTOs
{
    public class ClientReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string CompanyName { get; set; }

        public string? Email { get; set; }


    }
}
