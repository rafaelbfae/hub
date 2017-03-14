using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Domain.Interfaces.Filters
{
    public interface IDataTableFilter
    {
        int Length { get; set; }

        int Start { get; set; }

        int Order { get; set; }

        string Dir { get; set; }

        string Search { get; set; }
    }
}
