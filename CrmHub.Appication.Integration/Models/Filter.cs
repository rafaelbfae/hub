using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models
{
    public class Filter
    {
        public string Name { get; set; }
        public string Qualification { get; set; }
        public List<string> Dor { get; set; }
        public List<Question> Questions { get; set; }
    }
}
