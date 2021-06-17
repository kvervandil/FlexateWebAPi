using FlexateWebApi.Domain.Model;
using FlexateWebApi.Infrastructure.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Infrastructure.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly Context _context;
        private readonly ILogger<CarsRepository> _logger;

        public CarsRepository(Context context, ILogger<CarsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Car>> GetAllCars(CancellationToken cancellationToken)
        {
            return await _context.Cars.ToListAsync(cancellationToken);
        }

        public async Task<int> AddCar(Car car, CancellationToken cancellationToken)
        {
            await _context.Cars.AddAsync(car, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return car.Id;
        }

        public async Task<bool> DeleteCar(int id, CancellationToken cancellationToken)
        {
            var car = await GetCarById(id, cancellationToken);

            if (car == null)
            {
                return false;
            }

            _context.Remove(car);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Car> GetCarById(int id, CancellationToken cancellationToken)
        {
            return await _context.Cars.SingleOrDefaultAsync(car => car.Id == id, cancellationToken);

        }

        public async Task<List<Car>> GetCars(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            var cars = _context.Cars.AsQueryable();

            var carsFiltered = cars.Where(p => p.Model.StartsWith(searchString));

            return await carsFiltered.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<int> GetNoOfCars(CancellationToken cancellationToken)
        {
            return await _context.Cars.CountAsync(cancellationToken);
        }

        public async Task<bool> UpdateCar(Car car, CancellationToken cancellationToken)
        {
            try
            {
                var carToUpdate = await GetCarById(car.Id, cancellationToken);

                carToUpdate.Model = car.Model;
                carToUpdate.Brand = car.Brand;
                //carToUpdate.PersonId = car.PersonId;
                                
                await _context.SaveChangesAsync(cancellationToken);

                return true;
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
                var carToUpdate = await GetCarById(id, cancellationToken);

                carToUpdate.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public Task<List<Car>> GetCarsByPersonId(int personId, CancellationToken cancellationToken)
        {
            var personCars = _context.PersonCar.Where(personCar => personCar.PersonId == personId);

            var cars = _context.PersonCar.Where(personCar => personCar.PersonId == personId)
                .Select(item => item.Car);


            return cars.ToListAsync(cancellationToken);
        }
    }
}
