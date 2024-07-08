using Microsoft.AspNetCore.Identity;

namespace ECommerce.AuthApi.Models
{
    public class ECommerceUser:IdentityUser
    {
        public string Role { get; set; }
    }
}
