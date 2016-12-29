using CrmHub.Model.Crm;

namespace CrmHub.Model.User
{
    public class UserValue 
    {
        private CRM _crm;

        public UserValue()
        {
            this._crm = CRM.NONE;
        }

        public CRM crm 
        {
            get { return this._crm; }
            set { this._crm = value; }
        }

    }
}