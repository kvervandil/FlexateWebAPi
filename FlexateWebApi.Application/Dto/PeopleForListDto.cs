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
        public int PrevPage { get; set; }
        public int NextPage { get; set; }
        public int NoOfPage { get; set; }
    }
}
