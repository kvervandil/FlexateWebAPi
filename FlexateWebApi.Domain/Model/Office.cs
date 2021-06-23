using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Domain.Model
{
    public class Office
    {
        public int Id { get; set; }
        public string SpaceType { get; set; }
        public bool IsGroundFloor { get; set; }
        public bool IsDeleted { get; set; }
    }
}
