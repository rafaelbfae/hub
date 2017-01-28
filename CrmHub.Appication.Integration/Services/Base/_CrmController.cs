using CrmHub.Appication.Integration.Models;
using CrmHub.Appication.Integration.Models.Base;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Appication.Integration.Controllers.Base
{
    public abstract class _CrmController
    {
        #region Attributes

        private IMessageController _messageController;

        #endregion

        #region Constructor

        public _CrmController(IMessageController messageController)
        {
            _messageController = messageController;
        }

        #endregion

        #region Public Methods

        public IMessageController MessageController
        {
            get { return this._messageController; }
        }

        public virtual bool Schedule(Schedule value)
        {
            if (LeadRegister(value.Lead))
            {
                return OnSchedule(value);
            }

            return false;
        }

        #endregion

        #region Public Abstract Methods

        public abstract bool OnSchedule(Schedule value);
        public abstract bool ReSchedule(Schedule value);
        public abstract bool CancelSchedule(Schedule value);
        public abstract bool FeedBackSchedule(Schedule value);

        public abstract bool LeadRegister(Lead value);
        public abstract bool LeadUpdate(Lead value);
        public abstract bool LeadDelete(Lead value);

        public abstract bool ContactRegister(Contact value);
        public abstract bool ContactUpdate(Contact value);
        public abstract bool ContactDelete(Contact value);

        public abstract bool GetFields(EntityBase value);

        #endregion

    }
}
