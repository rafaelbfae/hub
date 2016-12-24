using System;

using CrmHub.Model.Crm;
using CrmHub.Model.Schedule;

namespace CrmHub.HubSpot 
{
    public class HubSpotController : CrmController
    {

        #region Const

        public static CRM crm = CRM.HUBSPOT;

        #endregion

        #region Public Methods

        public override bool CancelSchedule(ScheduleValue value)
        {
            throw new NotImplementedException();
        }

        public override bool FeedBackSchedule(ScheduleValue value)
        {
            throw new NotImplementedException();
        }

        public override bool ReSchedule(ScheduleValue value)
        {
            throw new NotImplementedException();
        }

        public override bool Schedule(ScheduleValue value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}