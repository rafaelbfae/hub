using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CrmHub.Identity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual int Level { get; set; }
    }
}
