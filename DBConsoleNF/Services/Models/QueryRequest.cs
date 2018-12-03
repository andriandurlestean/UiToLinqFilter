using System.Collections.Generic;

namespace DBConsoleNF.Services.Models
{
    public class QueryRequest
    {
        public string Search { get; set; }

        public IEnumerable<Filter> Filters { get; set; }

        public IEnumerable<Sort> SortBy { get; set; }

        public QueryRequest()
        {
            Filters = new List<Filter>();
            SortBy = new List<Sort>();
        }
    }
}
