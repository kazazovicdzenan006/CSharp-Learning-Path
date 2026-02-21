using Services.DTOs.BooksDtos;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Services.Validators.BooksValidators
{
    public class BookCreateDtoValidator : AbstractValidator<BooksCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(x => x.Naslov).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("Name is Required");
            RuleFor(x => x.Autor).MaximumLength(50);
            RuleFor(x => x.GodinaIzdanja).GreaterThan(0).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(x => x.BrojStranica).GreaterThan(0).LessThan(10000);
            RuleFor(x => x.GradId).GreaterThan(0);
        }

    }
}
