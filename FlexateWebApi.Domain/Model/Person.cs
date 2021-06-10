using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Domain.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Office> Offices { get; set; }
    }
}
