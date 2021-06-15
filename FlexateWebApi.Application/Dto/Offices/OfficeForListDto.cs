using AutoMapper;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Offices
{
    public class OfficeForListDto : IMapFrom<Office>
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int PersonId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Office, OfficeForListDto>();
        }
    }
}
