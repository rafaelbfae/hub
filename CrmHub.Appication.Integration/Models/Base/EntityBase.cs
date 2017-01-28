using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Models.Base
{
    public class EntityBase
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string EntityName { get; set; }

        public List<FieldMappingValue> Mapping { get; set; }

    }
}
