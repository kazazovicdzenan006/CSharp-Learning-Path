using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTOs.ClientDTOs
{
    public class ClientCreateDto
    {
        [Required(ErrorMessage = "Field Name is required")]
        [StringLength(100, MinimumLength = 2)] // input limitation 
        public string Name { get; set; }
        [Required(ErrorMessage = "Field Company Name is required")]
        public string CompanyName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")] // Checking Email address (Automated Proccess)
        public string? Email { get; set; }



    }
}
