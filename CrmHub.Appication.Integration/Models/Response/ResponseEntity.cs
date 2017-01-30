using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models.Response
{
    public class ResponseEntity
    {
        public ResponseEntity() 
        {
            this.Fields = new List<FieldCrm>();
        }

        public List<FieldCrm> Fields { get; set; }
        public string EntityName { get; set; }
    }
}