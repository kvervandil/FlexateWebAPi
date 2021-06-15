using AutoMapper;
using FlexateWebApi.Application.Dto.Cars;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain.Model;
using FlexateWebApi.Infrastructure.Entity.Interfaces;
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

        public CarsService(ICarsRepository carsRepository, IMapper mapper)
        {
            _carsRepository = carsRepository;
            _mapper = mapper;
        }

        public async Task<CarsForListDto> GetCars(int pageSize, int pageNo, string searchString,
                                                      CancellationToken cancellationToken)
        {
            var cars = await _carsRepository.GetCars(pageSize, pageNo, searchString, cancellationToken);

            var noOfCars = await _carsRepository.GetNoOfCars(cancellationToken);

            List<CarForListDto> carsDto = _mapper.Map<List<CarForListDto>>(cars);

            var carsForList = new CarsForListDto()
            {
                CarsList = carsDto,
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
                PersonId = carDto.PersonId,
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
                PersonId = carDto.PersonId
            };

            return await _carsRepository.UpdateCar(car, cancellationToken);
        }

        public async Task<bool> DeleteCar(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _carsRepository.DeleteCar(id, cancellationToken);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _carsRepository.UpdateWithDeletionFlag(id, cancellationToken);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
