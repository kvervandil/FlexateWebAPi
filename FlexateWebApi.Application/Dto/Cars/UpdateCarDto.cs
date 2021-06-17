using FlexateWebApi.Application.Mapping;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Cars
{
    public class UpdateCarDto : IMapFrom<Car>
    {
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}
