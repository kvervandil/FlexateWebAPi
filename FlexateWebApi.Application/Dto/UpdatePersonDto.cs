using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto
{
    public class UpdatePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
