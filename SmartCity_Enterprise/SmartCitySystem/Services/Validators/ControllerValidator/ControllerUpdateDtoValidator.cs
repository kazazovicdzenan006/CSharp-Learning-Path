using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.ControllersDtos;

namespace Services.Validators.ControllerValidator
{
    public class ControllerUpdateDtoValidator : AbstractValidator<ControllerUpdateDto>
    {
        public ControllerUpdateDtoValidator()
        {
            RuleFor(x => x.Status).NotNull().WithMessage("Status is required and can only be true or false");
            RuleFor(x => x.BrojKanala).GreaterThan(0).LessThan(9);
        }



    }
}
