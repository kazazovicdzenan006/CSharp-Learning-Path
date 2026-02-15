using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.CrossRoadDto;

namespace Services.Profiles
{
    public class CrossRoadProfile : Profile
    {

        public CrossRoadProfile()
        {
            CreateMap<CrossRoad, CrossRoadReadDto>();
            CreateMap<CrossRoadCreateDto, CrossRoad>();
            CreateMap<CrossRoadUpdateDto, CrossRoad>();
        }

    }
}
