using AutoMapper;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.People
{
    public class SinglePersonDto : IMapFrom<Person>
    {
        public int Age { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, UpdatePersonDto>();
        }
    }
}
