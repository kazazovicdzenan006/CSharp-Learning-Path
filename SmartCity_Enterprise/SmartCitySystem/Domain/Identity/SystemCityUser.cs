using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class SystemCityUser : IdentityUser<int> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public DateTime DateOfRegistration { get; set; } = DateTime.Now;



    }
}
