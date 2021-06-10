using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application.Dto
{
    public class PeopleForListDto
    {
        public List<PersonForListDto> PeopleList { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
        public int PrevPage {
            get
            {
                if (CurrentPage > 1)
                {
                    return CurrentPage--;
                }
                return 1;
            } 
        }
        public int NextPage { 
            get
            {
                return NoOfPage > CurrentPage ? CurrentPage++ : NoOfPage;
            }
        }
        public int NoOfPage => (int)Math.Round((double)Count / PageSize);
    }
}
