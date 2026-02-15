using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Models;
using Services.DTOs.ParkingLotDto;


namespace Services.Profiles
{
    public class ParkingLotProfile : Profile
    {

        public ParkingLotProfile()
        {
            CreateMap<ParkingLot, ParkingLotReadDto>();
            CreateMap<ParkingLotCreateDto, ParkingLot>();
            CreateMap<ParkingLotUpdateDto, ParkingLot>();
        }

    }
}
