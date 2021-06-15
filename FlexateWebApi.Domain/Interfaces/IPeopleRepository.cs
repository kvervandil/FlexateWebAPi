using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Infrastructure.Entity.Interfaces
{
    public interface IPeopleRepository
    {
        Task<List<Person>> GetPeople(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<Person> GetPersonById(int id, CancellationToken cancellationToken);
        Task<int> GetNoOfPeople(CancellationToken cancellationToken);
        Task<int> AddPerson(Person person, CancellationToken cancellationToken);
        Task<bool> UpdatePerson(Person person, CancellationToken cancellationToken);
        Task<bool> DeletePerson(int id, CancellationToken cancellationToken);
        Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken);
    }
}
