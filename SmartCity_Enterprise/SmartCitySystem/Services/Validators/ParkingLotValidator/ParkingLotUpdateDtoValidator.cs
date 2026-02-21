using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.ParkingLotDto;  

namespace Services.Validators.ParkingLotValidator
{
    public class ParkingLotUpdateDtoValidator : AbstractValidator<ParkingLotUpdateDto>
    {
        public ParkingLotUpdateDtoValidator()
        {
            RuleFor(x => x.CityZone).NotEmpty().MaximumLength(100);
            RuleFor(x => x.StreetName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ParkingName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TotalParkingSpots).GreaterThan(0).LessThan(10000).NotEmpty();
            RuleFor(x => x.AvailableParkingSpots).GreaterThanOrEqualTo(0).LessThanOrEqualTo(x => x.TotalParkingSpots)
                .NotEmpty().WithMessage("Available parking spots must be less than Total Parking spots");
        }
    }
}
