using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Cars
{
    class CreateCarDto : IMapFrom<Car>
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int PersonId { get; set; }
    }
}
