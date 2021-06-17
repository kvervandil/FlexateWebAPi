using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Domain.Model
{
    public class Office
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }

    }
}
