using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(SystemCityUser user, IList<string> roles);
    }
}
