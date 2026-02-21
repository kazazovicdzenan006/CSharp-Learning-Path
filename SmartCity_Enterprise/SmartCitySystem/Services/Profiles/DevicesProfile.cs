using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.DevicesDtos;

namespace Services.Profiles
{
    public class DevicesProfile : Profile
    {
        public DevicesProfile()
        {
            CreateMap<Uredjaj, DevicesReadDto>()
                .ForMember(dest => dest.GradName, opt => opt.MapFrom(src => src.Grad.Name))
                .ForMember(dest => dest.GradId, opt => opt.MapFrom(src => src.GradId));
        }
    }
}
