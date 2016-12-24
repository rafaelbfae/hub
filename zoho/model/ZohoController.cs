using System;

using CrmHub.Model.Crm;
using CrmHub.Model.Schedule;

namespace CrmHub.Zoho
{
    public class ZohoController : CrmController
    {
        #region Const

        public static CRM crm = CRM.ZOHO;

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