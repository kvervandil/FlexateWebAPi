using AutoMapper;
using FlexateWebApi.Application.Dto.People;
using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;

namespace FlexateWebApi.Application.Dto.Cars
{
    public class CarForListDto : IMapFrom<Car>
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int PersonId { get; set; }
        public virtual SinglePersonDto Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarForListDto>();
        }
    }
}