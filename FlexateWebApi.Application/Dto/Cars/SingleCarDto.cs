using AutoMapper;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Cars
{
    public class SingleCarDto : IMapFrom<Car>
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, SingleCarDto>();
        }
    }
}
