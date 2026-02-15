using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Microsoft.Identity.Client;
using Services.DTOs.GradDtos;

namespace Services.Profiles
{
   public class GradProfile : Profile
    {
        public GradProfile()
        {
            CreateMap<Grad, GradReadDto>();
            CreateMap<GradCreateDto, Grad>();
            CreateMap<GradUpdateDto, Grad>();
        }
    }
}
