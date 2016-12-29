using CrmHub.Infra.Messages;
using CrmHub.Model.Schedule;

namespace CrmHub.Model.Crm 
{
    public abstract class CrmController 
    {
        #region Attributes

        private HubController _controller;

        #endregion
        
        #region Constructor

        public CrmController(HubController controller)
        {
            this._controller = controller;
        }

        #endregion

        #region Public Methods

        public abstract bool Schedule(ScheduleValue value);
        public abstract bool ReSchedule(ScheduleValue value);
        public abstract bool CancelSchedule(ScheduleValue value);
        public abstract bool FeedBackSchedule(ScheduleValue value);

        public MessageController messageController()
        {
            return this._controller.messageController;
        }

        #endregion
    }
}