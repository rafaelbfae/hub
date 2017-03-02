using CrmHub.Application.Models.Exact.Roots.Base;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class EmpresaExact : BaseExact<EmpresaExact>
    {
        public Empresa Empresa { get; set; }
    }
}
