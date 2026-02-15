using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.SenzorDtos;

namespace Services.Profiles
{
    public class SenzorProfile : Profile
    {
        public SenzorProfile()
        {
            CreateMap<Senzor, SenzorReadDto>().ForMember(dest => dest.GradName, opt => opt.MapFrom(src => src.Grad.Name));
            CreateMap<SenzorCreateDto, Senzor>();
            CreateMap<SenzorUpdateDto, Senzor>();
        }


    }
}
