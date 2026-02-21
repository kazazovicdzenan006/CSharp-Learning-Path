using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.FilmDtos;
{
    
}

namespace Services.Validators.MovieValidator
{
   public class MovieUpdateDtoValidator : AbstractValidator<FilmUpdateDto>
    {
        public MovieUpdateDtoValidator()
        {
            RuleFor(x => x.Naslov).NotEmpty().When(x => x.Naslov != null).MinimumLength(3).MaximumLength(50).WithMessage("Name is Required");
            RuleFor(x => x.Reziser).MaximumLength(50);
            RuleFor(x => x.GodinaIzdanja).GreaterThan(0).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(x => x.TrajanjeUMinutama).GreaterThan(0).LessThan(500);
        }
    
    }
}
