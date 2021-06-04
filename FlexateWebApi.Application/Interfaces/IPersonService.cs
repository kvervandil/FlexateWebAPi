using FlexateWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Interfaces
{
    public interface IPersonService
    {
        IList<Person> GetAllPeople(int pageSize, int pageNo, string searchString);
    }
}
