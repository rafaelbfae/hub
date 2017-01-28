using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Models
{
    public class User
    {
        public User(Crm crm, string token)
        {
            this.Crm = crm;
            this.Token = token;
        }

        public Crm Crm { get; set; }

        public string Token { get; set; }
    }
}
