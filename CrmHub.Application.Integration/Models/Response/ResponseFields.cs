using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models.Response
{
    public class ResponseFields : ResponseCrm
    {
        public ResponseFields() : base() 
        {
            this.Entities = new List<ResponseEntity>();
        }

        public List<ResponseEntity> Entities { get; set; }

    }
}