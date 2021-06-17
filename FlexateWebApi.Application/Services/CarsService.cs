using AutoMapper;
using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.Cars;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain.Model;
using FlexateWebApi.Infrastructure.Entity.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Services
{
    public class CarsService : ICarsService
    {
        private ICarsRepository _carsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CarsService> _logger;

        public CarsService(ICarsRepository carsRepository, IMapper mapper, ILogger<CarsService> logger)
        {
            _carsRepository = carsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResultDto<SingleCarDto>> GetCars(int pageSize, int pageNo, string searchString,
                                                      CancellationToken cancellationToken)
        {
            var cars = await _carsRepository.GetCars(pageSize, pageNo, searchString, cancellationToken);

            var noOfCars = await _carsRepository.GetNoOfCars(cancellationToken);

            var carsDto = _mapper.Map<List<SingleCarDto>>(cars);

            var carsForList = new PagedResultDto<SingleCarDto>()
            {
                Items = carsDto,
                CurrentPage = pageNo,
                Count = noOfCars,
                PageSize = pageSize
            };
            return carsForList;
        }

        public async Task<SingleCarDto> GetCarById(int id, CancellationToken cancellationToken)
        {
            var car = await _carsRepository.GetCarById(id, cancellationToken);

            if (car == null || car.IsDeleted == true)
            {
                return null;
            }

            var carDto = _mapper.Map<SingleCarDto>(car);

            return carDto;
        }

        public async Task<int?> AddNewCar(CreateCarDto carDto, CancellationToken cancellationToken)
        {
            Car car = new Car()
            {
                Model = carDto.Model,
                Brand = carDto.Brand,
                IsDeleted = false
            };

            if (string.IsNullOrEmpty(car.Model)
                || string.IsNullOrEmpty(car.Brand))
            {
                return null;
            }

            int id = await _carsRepository.AddCar(car, cancellationToken);

            return id;
        }

        public async Task<bool> UpdateCar(int id, UpdateCarDto carDto,
                                             CancellationToken cancellationToken)
        {
            if (carDto == null)
            {
                return false;
            }

            Car car= new Car()
            {
                Id = id,
                Model = carDto.Model,
                Brand = carDto.Brand,
            };

            return await _carsRepository.UpdateCar(car, cancellationToken);
        }

        public async Task<bool> DeleteCar(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _carsRepository.DeleteCar(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _carsRepository.UpdateWithDeletionFlag(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<PagedResultDto<SingleCarDto>> GetAllCars(CancellationToken cancellationToken)
        {
            var cars = await _carsRepository.GetAllCars(cancellationToken);

            var carsDto = _mapper.Map<List<SingleCarDto>>(cars);

            var carsForList = new PagedResultDto<SingleCarDto>
            {
                Items = carsDto,
            };

            return carsForList;
        }
        public async Task<List<SingleCarDto>> GetCarsByPersonId(int personId, CancellationToken cancellationToken)
        {
            var cars = await _carsRepository.GetCarsByPersonId(personId, cancellationToken);

            List<SingleCarDto> carsForPersonDto = _mapper.Map<List<SingleCarDto>>(cars);

            return carsForPersonDto;
        }
    }
}
