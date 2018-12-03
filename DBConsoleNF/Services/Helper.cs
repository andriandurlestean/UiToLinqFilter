using System;
using System.Collections.Generic;
using System.Linq;
using DBConsoleNF.DataAccess.Models;
using DBConsoleNF.Services.Models;

namespace DBConsoleNF.Services
{
    public class Helper
    {
        public QueryRequest GenerateQueryRequest()
        {
            return new QueryRequest
            {
                Search = null,
                SortBy = new List<Sort>
                {
                    new Sort
                    {
                        Property = nameof(DocumentInfoViewModel.Name),
                        SortOrder = SortOperators.Ascendant
                    },
                    new Sort
                    {
                        Property = nameof(DocumentInfoViewModel.Price),
                        SortOrder = SortOperators.Descendant
                    }
                },
                Filters = new List<Filter>
                {
                  new Filter
                    {
                        Property =  nameof(DocumentInfoViewModel.Name),
                        Operator = FilterOperators.NotContains,
                        Value = "letin"
                    },
                    new Filter
                    {
                        Property =  nameof(DocumentInfoViewModel.Date),
                        Operator = FilterOperators.Greater,
                        Value = DateTime.UtcNow.AddMonths(-6).ToString()
                    },
                    new Filter
                    {
                        Property = nameof(DocumentInfoViewModel.IsDeleted),
                        Operator = FilterOperators.Equal,
                        Value = "false"
                    },
                    new Filter
                    {
                        Property = nameof(DocumentInfoViewModel.Price),
                        Operator = FilterOperators.Greater,
                        Value = "5"
                    },
                    new Filter
                    {
                        Property = nameof(DocumentInfoViewModel.Pages),
                        Operator = FilterOperators.Equal,
                        Value = "2"
                    }
                }
            };
        }
    }
}