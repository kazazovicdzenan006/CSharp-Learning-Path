using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Services.DTOs.WorkerDtos;
using Domain.Models;

namespace Services.Profiles
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<Worker, WorkerReadDto>();

            CreateMap<WorkerCreateDto, Worker>();

            CreateMap<WorkerUpdateDto, Worker>();

        }


    }
}
