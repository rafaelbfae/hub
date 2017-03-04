using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Integration.Models.Zoho
{

    public class User
    {
        public string zip { get; set; }
        public string country { get; set; }
        public string website { get; set; }
        public string role { get; set; }
        public string city { get; set; }
        public string timezone { get; set; }
        public string profile { get; set; }
        public string mobile { get; set; }
        public string language { get; set; }
        public string content { get; set; }
        public string zuid { get; set; }
        public string confirm { get; set; }
        public string phone { get; set; }
        public string street { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string status { get; set; }
    }

    public class Users
    {
        public List<User> user { get; set; }
    }

    public class RootUser
    {
        public Users users { get; set; }
    }

}
