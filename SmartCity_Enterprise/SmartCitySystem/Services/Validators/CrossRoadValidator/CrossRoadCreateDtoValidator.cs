using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.CrossRoadDto;

namespace Services.Validators.CrossRoadValidator
{
    public class CrossRoadCreateDtoValidator : AbstractValidator<CrossRoadCreateDto>
    {
        public CrossRoadCreateDtoValidator()
        {
            RuleFor(x => x.CityZone).NotEmpty().MaximumLength(100);
            RuleFor(x => x.StreetName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.CrossName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TrafficJamPercantage).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).NotEmpty().NotEmpty();
            RuleFor(x => x.GradId).GreaterThan(0).NotEmpty();   
        }


    }
}
