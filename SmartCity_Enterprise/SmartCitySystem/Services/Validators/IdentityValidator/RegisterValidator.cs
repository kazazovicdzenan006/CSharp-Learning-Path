using System;
using System.Collections.Generic;
using System.Text;
using Services.DTOs;

namespace Services.Validators.IdentityValidator
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).MinimumLength(3).MaximumLength(50).NotEmpty();
            RuleFor(x => x.LastName).MinimumLength(3).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(50)
                .WithMessage("Password must be at least 8 character")
                .Matches("[A-Z]").WithMessage("Password must contain uppercase")
                .Matches("[a-z]").WithMessage("Password must contain lowercase")
                .Matches(@"[0-9]+").WithMessage("Number required")
                .Matches(@"[\!\?\*\.]+").WithMessage("Special character (!?*.) required")
                .NotEmpty();
             RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match").NotEmpty();
        }


    }
}
