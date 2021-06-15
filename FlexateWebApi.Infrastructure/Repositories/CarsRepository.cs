using FlexateWebApi.Domain.Model;
using FlexateWebApi.Infrastructure.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public CarsRepository(Context context)
        {
            _context = context;
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
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Car> GetCarById(int id, CancellationToken cancellationToken)
        {
            return await _context.Cars.FindAsync(new object[] { id }, cancellationToken);

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
                _context.Attach(car);
                _context.Entry(car).Property("Model").IsModified = true;
                _context.Entry(car).Property("Branch").IsModified = true;
                _context.Entry(car).Property("PersonId").IsModified = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken)
        {
            try
            {
                var car = GetCarById(id, cancellationToken);

                _context.Attach(car);
                _context.Entry(car).Property("IsDeleted").IsModified = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
