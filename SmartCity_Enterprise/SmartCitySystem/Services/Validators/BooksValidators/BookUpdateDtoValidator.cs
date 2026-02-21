using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.DTOs.BooksDtos;

namespace Services.Validators.BooksValidators
{
    public class BookUpdateDtoValidator : AbstractValidator<BooksUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(x => x.Naslov).NotEmpty().When(x => x.Naslov != null).MinimumLength(3).MaximumLength(50).WithMessage("Name is Required");
            RuleFor(x => x.Autor).MaximumLength(50);
            RuleFor(x => x.GodinaIzdanja).GreaterThan(0).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(x => x.BrojStranica).GreaterThan(0).LessThan(10000);
        }


    }
}
