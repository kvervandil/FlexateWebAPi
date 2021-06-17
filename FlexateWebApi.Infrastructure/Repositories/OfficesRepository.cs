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
    public class OfficesRepository : IOfficesRepository
    {
        private readonly Context _context;
        private readonly ILogger<OfficesRepository> _logger;

        public OfficesRepository(Context context, ILogger<OfficesRepository> logger)
        {
            _context = context;
            _logger = logger;
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
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<int> GetNoOfOffices(CancellationToken cancellationToken)
        {
            return await _context.Offices.CountAsync(cancellationToken);
        }

        public async Task<Office> GetOfficeById(int id, CancellationToken cancellationToken)
        {
            return await _context.Offices.SingleOrDefaultAsync(office => office.Id == id, cancellationToken);
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
                var officeToUpdate = await GetOfficeById(office.Id, cancellationToken);

                officeToUpdate.City = office.City;
                officeToUpdate.Address = office.Address;
                officeToUpdate.PersonId = office.PersonId;

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
                var office = await GetOfficeById(id, cancellationToken);

                office.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
