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
        Person GetPersonById(int id);
        Person AddNewPerson(CreatePersonDto personDto);
        bool UpdatePerson(int personToUpdateId, UpdatePersonDto personDto);
        bool DeletePerson(int id);
        bool UpdateWithDeleteFlag(int id);
    }
}
