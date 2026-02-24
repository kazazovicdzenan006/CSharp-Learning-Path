using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.IdentityDto
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
