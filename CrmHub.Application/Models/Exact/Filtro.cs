using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class Filtro
    {
        public string Nome { get; set; }
        public string Qualificacao { get; set; }
        public List<string> Dor { get; set; }
        public List<Pergunta> Perguntas { get; set; }
    }
}
