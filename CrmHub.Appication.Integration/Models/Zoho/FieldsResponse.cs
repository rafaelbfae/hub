using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models.Zoho
{
    public class FieldsResponse
    {
        public class FL
        {
            public List<string> val { get; set; }
            public string dv { get; set; }
            public bool customfield { get; set; }
            public int maxlength { get; set; }
            public string isreadonly { get; set; }
            public string fval { get; set; }
            public string label { get; set; }
            public string type { get; set; }
            public bool enabled { get; set; }
            public bool req { get; set; }
        }

        public class Section
        {
            public string dv { get; set; }
            public List<FL> FL { get; set; }
            public string name { get; set; }
        }

        public class Events : EntityResponse { }

        public class Contacts : EntityResponse { }

        public class Leads : EntityResponse { }

        public class Accounts : EntityResponse { }

        public class Potentials : EntityResponse { }

        public class EntityResponse
        {
            public List<Section> section { get; set; }
        }

        public class FieldsResponseCrm : object
        {
            public Leads Leads { get; set; }
            public Events Events { get; set; }
            public Contacts Contacts { get; set; }
            public Accounts Accounts { get; set; }
            public Potentials Potentials { get; set; }
        }
    }
}
