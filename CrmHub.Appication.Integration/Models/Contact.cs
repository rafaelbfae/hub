using CrmHub.Application.Integration.Models.Base;
using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string MessengerId { get; set; }
        public string MessengerType { get; set; }
        public List<string> Phones { get; set; }
    }
}
