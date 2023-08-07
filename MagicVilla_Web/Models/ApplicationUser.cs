using Microsoft.AspNetCore.Identity;

namespace MagicVilla_Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
