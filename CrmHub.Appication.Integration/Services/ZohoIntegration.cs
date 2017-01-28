using CrmHub.Appication.Integration.Enuns;
using CrmHub.Appication.Integration.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrmHub.Appication.Integration.Models;
using CrmHub.Appication.Integration.Models.Base;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Appication.Integration.Services
{
    public class ZohoIntegration : ICrmIntegration
    {
        public static eCrmName CRM_NAME = eCrmName.ZOHOCRM;

        #region Attributes

        private IMessageController _messageController;

        #endregion

        public ZohoIntegration(IMessageController messageController)
        {
            _messageController = messageController;
        }

        public IMessageController MessageController
        {
            get
            {
                return _messageController;
            }
        }

        public bool CancelSchedule(Schedule value)
        {
            throw new NotImplementedException();
        }

        public bool ContactDelete(Contact value)
        {
            throw new NotImplementedException();
        }

        public bool ContactRegister(Contact value)
        {
            throw new NotImplementedException();
        }

        public bool ContactUpdate(Contact value)
        {
            throw new NotImplementedException();
        }

        public bool FeedBackSchedule(Schedule value)
        {
            throw new NotImplementedException();
        }

        public bool GetFields(EntityBase value)
        {
            throw new NotImplementedException();
        }

        public bool LeadDelete(Lead value)
        {
            throw new NotImplementedException();
        }

        public bool LeadRegister(Lead value)
        {
            throw new NotImplementedException();
        }

        public bool LeadUpdate(Lead value)
        {
            throw new NotImplementedException();
        }

        public bool OnSchedule(Schedule value)
        {
            throw new NotImplementedException();
        }

        public bool ReSchedule(Schedule value)
        {
            var name = value.Subject;
            return true;
        }

        public bool Schedule(Schedule value)
        {
            var name = value.Subject;
            return true;
        }
    }
}
