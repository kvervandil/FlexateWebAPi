using FlexateWebApi.Application.Dto;
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

        public Person GetPersonById(int id)
        {
            var person = People.FirstOrDefault(e => e.Id == id);

            return person;
        }

        public Person AddNewPerson(CreatePersonDto personDto)
        {
            Person person = new Person()
            {
                Name = personDto.Name,
                IsDeleted = false,
                Id = GetLastPersonId() + 1
            };

            People.Add(person);

            return person;
        }

        private int GetLastPersonId()
        {
            int lastId = People.LastOrDefault().Id;

            return lastId;
        }

        public void UpdatePerson(int personToUpdateId, UpdatePersonDto personDto)
        {
            var personToUpdate = People.FirstOrDefault(p => p.Id == personToUpdateId);

            personToUpdate.Name = personDto.Name;
        }

        public void DeletePerson(int id)
        {
            var person = People.FirstOrDefault(person => person.Id == id);

            if (person != null)
            {
                People.Remove(person);
            }
        }
    }
}
