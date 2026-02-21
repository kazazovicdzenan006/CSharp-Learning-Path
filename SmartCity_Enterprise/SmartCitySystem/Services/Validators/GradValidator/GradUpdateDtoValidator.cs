using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.GradDtos;    

namespace Services.Validators.GradValidator
{
    public class GradUpdateDtoValidator : AbstractValidator<GradUpdateDto>
    {

        public GradUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
