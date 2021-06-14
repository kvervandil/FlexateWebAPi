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
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public bool IsDeleted { get; set; }
    }
}
