using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Entity.Model
{
    public class PersonCar
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CarId { get; set; }
        public string Vin { get; set; }
        public bool IsDeleted { get; set; }
        public string Color { get; set; }

        public virtual Person Person { get; set; }
        public virtual Car Car { get; set; }
            
    }
}
