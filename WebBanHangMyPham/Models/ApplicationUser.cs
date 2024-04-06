using Microsoft.AspNetCore.Identity;

namespace WebBanHangMyPham.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
