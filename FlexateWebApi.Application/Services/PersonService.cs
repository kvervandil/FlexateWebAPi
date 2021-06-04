using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexateWebApi.Application.Services
{
    public class PersonService : IPersonService
    {
        public IList<Person> People { get; set; }

        public PersonService()
        {
            People = Entity.InitializePeople();
        }

        public IList<Person> GetAllPeople(int pageSize, int pageNo, string searchString)
        {
            var people = People.Where(p => p.Name.StartsWith(searchString));

            var peopleToShow = people.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            return peopleToShow;
        }
    }
}
