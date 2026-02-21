using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.SenzorDtos;

namespace Services.Validators.SenzorValidator
{
    public class SenzorUpdateDtoValidator : AbstractValidator<SenzorUpdateDto>
    {
        public SenzorUpdateDtoValidator()
        {
            RuleFor(x => x.Vrijednost).NotEmpty().GreaterThan(0).LessThan(1000);
        }


    }
}
