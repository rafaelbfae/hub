using CrmHub.Appication.Integration.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Models
{
    public class Schedule : EntityBase
    {
        public string Subject { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Venue { get; set; }

        public bool FlNotification { get; set; }

        public Lead Lead { get; set; }
        
    }
}
