using FlexateWebApi.Domain.Interfaces;
using FlexateWebApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
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

        public PeopleRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetPeople(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            var people = _context.People.AsQueryable();

            var peopleFiltered = people.Where(p => p.Name.StartsWith(searchString));
            
            return await peopleFiltered.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<Person> GetPersonById(int id, CancellationToken cancellationToken)
        {
            return await _context.People.FindAsync(id, cancellationToken);
        }

        public async Task<int> GetNoOfPeople(CancellationToken cancellationToken)
        {
            return await _context.People.CountAsync(cancellationToken);
        }

        public async Task<int> AddPerson(Person person, CancellationToken cancellationToken)
        {
            await _context.People.AddAsync(person, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return person.Id;
        }

        public async Task<bool> UpdatePerson(Person person, CancellationToken cancellationToken)
        {
            try
            {
                _context.Attach(person);
                _context.Entry(person).Property("Name").IsModified = true;
                _context.Entry(person).Property("Address").IsModified = true;
                _context.Entry(person).Property("Age").IsModified = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (System.Exception)
            {
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

            _context.Remove(person);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
