
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.SenzorDtos;

namespace Services.Validators.SenzorValidator
{
    public class SenzorCreateDtoValidator : AbstractValidator<SenzorCreateDto>
    {
        public SenzorCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("Name is Required");
            RuleFor(x => x.Vrijednost).NotEmpty().GreaterThan(0).LessThan(1000);
            RuleFor(x => x.GradId).NotEmpty().GreaterThan(0);
        }



    }
}
