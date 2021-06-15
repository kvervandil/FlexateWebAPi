using AutoMapper;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Cars
{
    public class CreateCarDto : IMapFrom<Car>
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int PersonId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CreateCarDto>();
        }
    }
}
