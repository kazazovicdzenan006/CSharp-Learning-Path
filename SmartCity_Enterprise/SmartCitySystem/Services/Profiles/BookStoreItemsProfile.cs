using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.BookStoreItemsDto;

namespace Services.Profiles
{
    public class BookStoreItemsProfile : Profile
    {

        public BookStoreItemsProfile()
        {
            CreateMap<BibliotekaArtikal, BookStoreItemsReadDto>().ForMember(dest => dest.GradId, opt => opt.MapFrom(src => src.Grad.Name));
        }

    }
}
