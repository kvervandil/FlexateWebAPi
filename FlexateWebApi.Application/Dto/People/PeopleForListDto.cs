using AutoMapper;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.People
{
    public class PeopleForListDto : IMapFrom<Person>
    {
        public List<PersonForListDto> PeopleList { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
        public bool IsFirstPage { get 
            { return CurrentPage == 1; }
        }
        public bool IsLastPage { get 
            { return NoOfPages == CurrentPage; }
        }
        public int NoOfPages => (int)Math.Ceiling((double)Count / PageSize);

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PeopleForListDto>();
        }
    }
}
