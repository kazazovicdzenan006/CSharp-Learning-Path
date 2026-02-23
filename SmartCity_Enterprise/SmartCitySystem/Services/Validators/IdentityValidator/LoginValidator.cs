using System;
using System.Collections.Generic;
using System.Text;
using Services.DTOs;

namespace Services.Validators.IdentityValidator
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(50).NotEmpty();
        }


    }
}
