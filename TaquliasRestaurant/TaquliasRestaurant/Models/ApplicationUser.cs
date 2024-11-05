using Microsoft.AspNetCore.Identity;

namespace TaquliasRestaurant.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Order>? Orders { get; set; } 
    }
}
