using AutoMapper;
using FlexateWebApi.Application.Dto.Cars;
using FlexateWebApi.Domain.Model;

namespace FlexateWebApi.Application.Mapping
{
    class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CreateCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();
        }
    }
}
