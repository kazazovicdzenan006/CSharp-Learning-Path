using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.CityNodeDto;


namespace Services.Profiles
{
    public class CityNodeProfile : Profile
    {

        public CityNodeProfile()
        {
            CreateMap<CityNode, CityNodeReadDto>()
                .ForMember(dest => dest.GradName, opt => opt.MapFrom(src => src.Grad.Name)) 
        .ForMember(dest => dest.GradId, opt => opt.MapFrom(src => src.GradId))
        .IncludeAllDerived(); 
        }

    }
}
