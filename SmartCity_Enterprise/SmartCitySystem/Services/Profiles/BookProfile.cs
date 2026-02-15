using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.BooksDtos;

namespace Services.Profiles
{
    public class BookProfile : Profile
    {

        public BookProfile()
        {
            CreateMap<Knjiga, BooksReadDto>();
            CreateMap<BooksCreateDto, Knjiga>();
            CreateMap<BooksUpdateDto, Knjiga>();
        }

    }
}
