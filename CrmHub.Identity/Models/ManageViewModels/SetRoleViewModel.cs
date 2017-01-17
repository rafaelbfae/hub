using System.ComponentModel.DataAnnotations;

namespace CrmHub.Identity.Models.ManageViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}