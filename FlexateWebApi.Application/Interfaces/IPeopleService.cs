using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.People;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Interfaces
{
    public interface IPeopleService
    {
        Task<GenericForListDto<PersonForListDto>> GetPeople(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<SinglePersonDto> GetPersonById(int id, CancellationToken cancellationToken);
        Task<int?> AddNewPerson(CreatePersonDto personDto, CancellationToken cancellationToken);
        Task<bool> UpdatePerson(int id, UpdatePersonDto personDto, CancellationToken cancellationToken);
        Task<bool> DeletePerson(int id, CancellationToken cancellationToken);
        Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken);
    }
}
