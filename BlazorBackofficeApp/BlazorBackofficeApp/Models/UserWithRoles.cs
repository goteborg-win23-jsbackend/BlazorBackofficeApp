using BlazorBackofficeApp.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorBackofficeApp.Models
{
    public class UserWithRoles
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }

        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
