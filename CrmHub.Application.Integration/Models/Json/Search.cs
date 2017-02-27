using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Integration.Models.Json
{

    public class FL
    {
        public string val { get; set; }
        public string content { get; set; }
    }

    public class Row
    {
        public string no { get; set; }
        public FL FL { get; set; }
    }

    public class Contacts
    {
        public Row row { get; set; }
    }

    public class Accounts
    {
        public Row row { get; set; }
    }

    public class Potentials
    {
        public Row row { get; set; }
    }

    public class Result
    {
        public Contacts Contacts { get; set; }
        public Accounts Accounts { get; set; }
        public Potentials Potentials { get; set; }
    }

    public class Response
    {
        public Result result { get; set; }
        public string uri { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
    }

}
