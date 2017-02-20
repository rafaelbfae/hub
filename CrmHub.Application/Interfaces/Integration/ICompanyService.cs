using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface ICompanyService : IMessageService
    {
        bool Register(EmpresaExact value);
        bool Update(EmpresaExact value);
        bool Fields(Autenticacao value);
        bool Delete(string id, Autenticacao value);
    }
}
