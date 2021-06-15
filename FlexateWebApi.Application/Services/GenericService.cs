using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Services
{
    public class GenericService<T> where T : class
    {
        public List<T> items;
        private int pageSize;
        private int pageNo;
        private string searchString;

        public GenericService()
        {
            items = new List<T>();
        }

        public void Paginate()
        {

        }

    }
}
