using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Services.DTOs.FilmDtos;


namespace Services.Profiles
{
    public class FilmProfile : Profile
    {

        public FilmProfile()
        {
            CreateMap<Film, FilmReadDto>();
            CreateMap<FilmCreateDto, Film>();
            CreateMap<FilmUpdateDto, Film>();
        }

    }
}
