using AutoMapper;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Offices
{
    public class CreateOfficeDto : IMapFrom<Office>
    {
        public string SpaceType { get; set; }
        public bool IsGroundFloor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Office, CreateOfficeDto>();
        }
    }
}
