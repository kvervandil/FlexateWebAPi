using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Domain.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool IsDeleted { get; set; }
    }
}
