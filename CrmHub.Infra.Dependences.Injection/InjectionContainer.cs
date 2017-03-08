using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Application.Interfaces;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Services;
using CrmHub.Application.Services.Integration;
using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Infra.Data.Repositories;
using CrmHub.Infra.Helpers;
using CrmHub.Infra.Helpers.Interfaces;
using CrmHub.Infra.Messages;
using CrmHub.Infra.Messages.Interfaces;
using LogApiHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CrmHub.Infra.Dependences.Injection
{
    public class InjectionContainer
    {
        public InjectionContainer(IServiceCollection services)
        {
            this.CreateSimpleInject(services);
        }

        private void CreateSimpleInject(IServiceCollection services)
        {
            #region Integration

            services.AddTransient<IHubIntegration, HubIntegration>();
            services.AddTransient<IHubService, HubService> ();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ILeadService, LeadService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEventService, EventService>();

            #endregion

            services.AddTransient<ICrmRepository, CrmRepository>();
            services.AddTransient<ILogApiRepository, LoggerApiRepository>();
            services.AddTransient<ICrmService, CrmService>();
            services.AddTransient<ILoggerApiService, LoggerApiService>();

            services.AddTransient<IMessageController, MessageController>();

            services.AddTransient<IHttpMessageSender, HttpMessageSender>();
        }
    }
}
