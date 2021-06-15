using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexateWebApi.Infrastructure.Repositories
{
    public class GenericRepository<T>
    {
        private readonly IQueryable<T> _items;

        public GenericRepository(IQueryable<T> items)
        {
            _items = items;
        }

        public IQueryable<T> Paginate(int pageSize, int pageNo)
        {
            return _items.Skip(pageSize * (pageNo - 1)).Take(pageSize);
        }
    }
}
