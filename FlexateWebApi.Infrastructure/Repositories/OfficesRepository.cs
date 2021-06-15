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
    public class OfficesRepository : IOfficesRepository
    {
        private readonly Context _context;

        public OfficesRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Office>> GetAllOffices(CancellationToken cancellationToken)
        {
            return await _context.Offices.ToListAsync(cancellationToken);
        }

        public async Task<int> AddOffice(Office office, CancellationToken cancellationToken)
        {
            await _context.Offices.AddAsync(office, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return office.Id;
        }

        public async Task<bool> DeleteOffice(int id, CancellationToken cancellationToken)
        {
            var car = await GetOfficeById(id, cancellationToken);

            if (car == null)
            {
                return false;
            }

            _context.Remove(car);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetNoOfOffices(CancellationToken cancellationToken)
        {
            return await _context.Offices.CountAsync();
        }

        public async Task<Car> GetOfficeById(int id, CancellationToken cancellationToken)
        {
            return await _context.Cars.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<List<Office>> GetOffices(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            var offices = _context.Offices.AsQueryable();

            var officesFiltered = offices.Where(p => p.City.StartsWith(searchString));

            return await officesFiltered.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateOffice(Office office, CancellationToken cancellationToken)
        {
            try
            {
                _context.Attach(office);
                _context.Entry(office).Property("City").IsModified = true;
                _context.Entry(office).Property("Address").IsModified = true;
                _context.Entry(office).Property("PersonId").IsModified = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
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
                var office = GetOfficeById(id, cancellationToken);

                _context.Attach(office);
                _context.Entry(office).Property("IsDeleted").IsModified = true;

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
