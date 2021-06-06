using FlexateWebApi.Application.Dto;
using FlexateWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Interfaces
{
    public interface IPersonService
    {
        IList<Person> GetAllPeople(int pageSize, int pageNo, string searchString);
        Person GetPersonById(int id);
        Person AddNewPerson(CreatePersonDto personDto);
        void UpdatePerson(int personToUpdateId, UpdatePersonDto personDto);
        void DeletePerson(int id);
    }
}
