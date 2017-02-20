using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Application.Interfaces;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Services;
using CrmHub.Application.Services.Integration;
using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Infra.Data.Repositories;
using CrmHub.Infra.Messages;
using CrmHub.Infra.Messages.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrmHub.Infra.Dependences.Injection
{
    public class InjectionContainer
    {
        public InjectionContainer(IServiceCollection services)
        {
            this.CreateSimpleInject(services);
        }

        public void CreateSimpleInject(IServiceCollection services)
        {
            services.AddTransient<IHubIntegration, HubIntegration>();
            services.AddTransient<ICrmService, CrmService>();

            services.AddTransient<IHubService, HubService> ();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ILeadService, LeadService>();
            services.AddTransient<ICompanyService, CompanyService>();

            services.AddTransient<ICrmRepository, CrmRepository>();

            services.AddTransient<IMessageController, MessageController>();


        }
    }
}
