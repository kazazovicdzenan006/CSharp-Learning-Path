using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Services.DTOs.ClientDTOs;
using Domain.Models;

namespace Services.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile() {
            CreateMap<Client, ClientReadDto>();

            CreateMap<ClientCreateDto, Client>();

            CreateMap<ClientUpdateDto, Client>();

        }


    }
}
