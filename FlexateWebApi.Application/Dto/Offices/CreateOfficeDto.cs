using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto.Offices
{
    public class CreateOfficeDto
    {
        public string City { get; set; }
        public string Address { get; set; }
        public int PersonId { get; set; }
    }
}
