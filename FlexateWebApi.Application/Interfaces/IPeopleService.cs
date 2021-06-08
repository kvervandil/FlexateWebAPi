using FlexateWebApi.Application.Dto;
using FlexateWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Interfaces
{
    public interface IPeopleService
    {
        Task<PeopleForListDto> GetPeople(int pageSize, int pageNo, string searchString);
        Task<Person> GetPersonById(int id);
        Task<Person> AddNewPerson(CreatePersonDto personDto);
        Task<bool> UpdatePerson(int personToUpdateId, UpdatePersonDto personDto);
        Task<bool> DeletePerson(int id);
        Task<bool> UpdateWithDeleteFlag(int id);
    }
}
