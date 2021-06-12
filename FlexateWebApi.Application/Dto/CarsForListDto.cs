using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto
{
    public class CarsForListDto
    {
        public List<CarForListDto> PeopleList { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
        public bool IsFirstPage
        {
            get
            {
                return CurrentPage == 1;
            }
        }
        public bool IsLastPage
        {
            get
            {
                return NoOfPages == CurrentPage;
            }
        }
        public int NoOfPages => (int)Math.Ceiling((double)Count / PageSize);
    }
}
