using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.ControllersDtos;

namespace Services.Validators.ControllerValidator
{
    public class ControllerCreateDtoValidator : AbstractValidator<ControllerCreateDto>
    {
        public ControllerCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ModelKontrolera).NotEmpty().MaximumLength(100); 
            RuleFor(x => x.Status).NotNull().WithMessage("Status is required and must be either true or false.");
            RuleFor(x => x.BrojKanala).GreaterThan(0).LessThan(9).NotEmpty();
            RuleFor(x => x.GradId).GreaterThan(0).NotEmpty();
        }


    }
}
