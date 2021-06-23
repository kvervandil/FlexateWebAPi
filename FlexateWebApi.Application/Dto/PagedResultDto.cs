using System;
using System.Collections.Generic;

namespace FlexateWebApi.Application.Dto
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => NoOfPages == CurrentPage;
        public int NoOfPages => (int)Math.Ceiling((double)Count / PageSize);
    }
}
