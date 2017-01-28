using CrmHub.Application.Models.Exact.Roots.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class ScheduleHub : BaseHub
    {
        public Lead Lead { get; set; }
        public Reuniao Reuniao { get; set; }
        public Endereco Endereco { get; set; }
        public List<Contato> Contatos { get; set; }

    }
}
