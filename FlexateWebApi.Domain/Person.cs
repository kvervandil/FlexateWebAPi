using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
