﻿using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.Cars;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Interfaces
{
    public interface ICarsService
    {
        Task<PagedResultDto<SingleCarDto>> GetCars(int pageSize, int pageNo, string searchString,
                                                      CancellationToken cancellationToken);
        Task<SingleCarDto> GetCarById(int id, CancellationToken cancellationToken);
        Task<int?> AddNewCar(CreateCarDto carDto, CancellationToken cancellationToken);
        Task<bool> UpdateCar(int id, UpdateCarDto carDto,
                                             CancellationToken cancellationToken);
        Task<bool> DeleteCar(int id, CancellationToken cancellationToken);
        Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken);
        Task<PagedResultDto<SingleCarDto>> GetAllCars(CancellationToken cancellationToken);
        Task<List<SingleCarDto>> GetCarsByPersonId(int personId, CancellationToken cancellationToken);
    }
}
