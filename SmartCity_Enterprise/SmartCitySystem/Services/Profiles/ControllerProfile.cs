using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.ControllersDtos;

namespace Services.Profiles
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<Kontroler, ControllerReadDto>();
            CreateMap<ControllerCreateDto, Kontroler>();
            CreateMap<ControllerUpdateDto, Kontroler>();
        }
    }
}
