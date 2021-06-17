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
    public class PeopleRepository : IPeopleRepository
    {
        private readonly Context _context;
        private readonly ILogger<PeopleRepository> _logger;

        public PeopleRepository(Context context, ILogger<PeopleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Person>> GetPeople(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            var people = _context.People.AsQueryable();

            //var peopleFiltered = new GenericRepository<Person>(people).Paginate(pageSize, pageNo);

            var peopleFiltered = people.Where(p => p.Name.StartsWith(searchString));

            return await peopleFiltered.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync(cancellationToken);

        }

        public async Task<Person> GetPersonById(int id, CancellationToken cancellationToken)
        {
            return await _context.People.SingleOrDefaultAsync(person => person.Id == id, cancellationToken);
        }

        public async Task<int> GetNoOfPeople(CancellationToken cancellationToken)
        {
            return await _context.People.CountAsync(cancellationToken);
        }

        public async Task<int?> AddPerson(Person person, CancellationToken cancellationToken)
        {
            if (person == null)
            {
                return null;
            }

            try
            {
                await _context.People.AddAsync(person, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return person.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }            
        }

        public async Task<bool> UpdatePerson(Person person, CancellationToken cancellationToken)
        {
            try
            {
                var personToUpdate = await GetPersonById(person.Id, cancellationToken);

                personToUpdate.Name = person.Name;
                personToUpdate.Address = person.Address;
                personToUpdate.Age = person.Age;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> DeletePerson(int id, CancellationToken cancellationToken)
        {
            var person = await GetPersonById(id, cancellationToken);

            if (person == null)
            {
                return false;
            }

            try
            {
                _context.Remove(person);
                await _context.SaveChangesAsync();

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
                var person = await GetPersonById(id, cancellationToken);

                person.IsDeleted = true;

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
