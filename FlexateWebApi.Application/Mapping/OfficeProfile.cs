using AutoMapper;
using FlexateWebApi.Application.Dto.Offices;
using FlexateWebApi.Domain.Model;

namespace FlexateWebApi.Application.Mapping
{
    class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<CreateOfficeDto, Office>();
            CreateMap<UpdateOfficeDto, Office>();
        }
    }
}
