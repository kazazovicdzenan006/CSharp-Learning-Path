using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.FilmDtos;

namespace Services.Validators.MovieValidator
{
    public class MovieCreateDtoValidator : AbstractValidator<FilmCreateDto>
    {
        public MovieCreateDtoValidator()
        {
            RuleFor(x => x.Naslov).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("Name is Required");
            RuleFor(x => x.Reziser).MaximumLength(50).NotEmpty();
            RuleFor(x => x.GodinaIzdanja).GreaterThan(0).LessThanOrEqualTo(DateTime.Now.Year).NotEmpty();
            RuleFor(x => x.TrajanjeUMinutama).GreaterThan(0).LessThan(500).NotEmpty();
            RuleFor(x => x.GradId).GreaterThan(0).NotEmpty();
        }



    }
}
