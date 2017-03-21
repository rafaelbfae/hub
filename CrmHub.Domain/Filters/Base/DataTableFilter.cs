using CrmHub.Domain.Interfaces.Filters;
using System;

namespace CrmHub.Domain.Filters.Base
{
    public class DataTableFilter : IDataTableFilter
    {
        public int Length { get; set; }

        public int Start { get; set; }

        public int Order { get; set; }

        public int Total { get; set; }

        public string Dir { get; set; }

        public string Search { get; set; }
    }
}
