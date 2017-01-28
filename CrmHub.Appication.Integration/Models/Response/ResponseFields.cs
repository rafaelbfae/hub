using System.Collections.Generic;

namespace CrmHub.Appication.Integration.Models.Response
{
    public class ResponseFields : ResponseCrm
    {
        private List<ResponseEntity> _entities;

        public ResponseFields() : base()
        {
            this.Entities = new List<ResponseEntity>();
        }

        public List<ResponseEntity> Entities { get; set; }

    }
}
