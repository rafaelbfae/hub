using CrmHub.Appication.Integration.Models;
using CrmHub.Appication.Integration.Models.Base;
using CrmHub.Infra.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Interfaces.Base
{
    public interface ICrmIntegration
    {
        IMessageController MessageController { get; }

        bool Schedule(Schedule value);
        bool OnSchedule(Schedule value);
        bool ReSchedule(Schedule value);
        bool CancelSchedule(Schedule value);
        bool FeedBackSchedule(Schedule value);

        bool LeadRegister(Lead value);
        bool LeadUpdate(Lead value);
        bool LeadDelete(Lead value);

        bool ContactRegister(Contact value);
        bool ContactUpdate(Contact value);
        bool ContactDelete(Contact value);

        bool GetFields(EntityBase value);
    }
}
