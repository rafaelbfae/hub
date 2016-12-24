using CrmHub.Model.Schedule;

namespace CrmHub.Model.Crm 
{
    public abstract class CrmController 
    {
        #region Public Methods

        public abstract bool Schedule(ScheduleValue value);
        public abstract bool ReSchedule(ScheduleValue value);
        public abstract bool CancelSchedule(ScheduleValue value);
        public abstract bool FeedBackSchedule(ScheduleValue value);

        #endregion
    }
}