using AutoMapper;
using FlexateWebApi.Application.Dto.People;
using FlexateWebApi.Domain.Model;

namespace FlexateWebApi.Application.Mapping
{
    class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<CreatePersonDto, Person>();
            CreateMap<UpdatePersonDto, Person>();
        }
    }
}
