using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Infrastructure.Entity.Interfaces
{
    public interface IOfficesRepository
    {
        Task<List<Office>> GetOffices(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<List<Office>> GetAllOffices(CancellationToken cancellationToken);
        Task<Office> GetOfficeById(int id, CancellationToken cancellationToken);
        Task<int> GetNoOfOffices(CancellationToken cancellationToken);
        Task<int> AddOffice(Office office, CancellationToken cancellationToken);
        Task<bool> UpdateOffice(Office office, CancellationToken cancellationToken);
        Task<bool> DeleteOffice(int id, CancellationToken cancellationToken);
        Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken);
    }
}
