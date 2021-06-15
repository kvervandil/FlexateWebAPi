using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.Offices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Interfaces
{
    public interface IOfficesService
    {
        Task<GenericForListDto<OfficeForListDto>> GetOffices(int pageSize, int pageNo, string searchString,
                                                      CancellationToken cancellationToken);
        Task<SingleOfficeDto> GetOfficeById(int id, CancellationToken cancellationToken);
        Task<int?> AddNewOffice(CreateOfficeDto officeDto, CancellationToken cancellationToken);
        Task<bool> UpdateOffice(int id, UpdateOfficeDto officeDto,
                                             CancellationToken cancellationToken);
        Task<bool> DeleteOffice(int id, CancellationToken cancellationToken);
        Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken);
        Task<OfficesForListDto> GetAllOffices(CancellationToken cancellationToken);
    }
}
