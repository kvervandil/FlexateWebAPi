using FlexateWebApi.Domain;
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
        public bool IsFirstPage { get {
                if (CurrentPage == 1)
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsLastPage { get {
                if (NoOfPages == CurrentPage)
                {
                    return true;
                }
                return false;
            } }
        public int NoOfPages => (int)Math.Ceiling((double)Count / PageSize);
    }
}
