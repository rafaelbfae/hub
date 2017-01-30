﻿using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Application.Interfaces;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Services;
using CrmHub.Application.Services.Integration;
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
            services.AddTransient<ICrmIntegration, HubIntegration>();
            services.AddTransient<ICrmService, CrmService>();

            services.AddTransient<IHubService, HubService> ();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IMessageController, MessageController>();


        }
    }
}
