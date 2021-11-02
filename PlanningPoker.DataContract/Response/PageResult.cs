using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Response
{
    public class PageResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int HeaderCode { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
