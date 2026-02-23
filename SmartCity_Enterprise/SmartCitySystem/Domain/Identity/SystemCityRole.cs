using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class SystemCityRole : IdentityRole<int>
    {

        public SystemCityRole() : base() { }

        public SystemCityRole(string RoleName) : base(RoleName) { }
   
        public string? RoleDescription { get; set; }


    }
}
