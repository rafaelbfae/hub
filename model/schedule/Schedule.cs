using System.ComponentModel.DataAnnotations;

using CrmHub.Model.User;

namespace CrmHub.Model.Schedule 
{
    public class ScheduleValue 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }

        public UserValue user { get; set; }
    }
}