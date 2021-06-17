using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Entity.Model
{
    public class PersonOffice
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public bool IsDeleted { get; set; }

        public int PersonId { get; set; }
        public int OfficeId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Office Office { get; set; }
    }
}
