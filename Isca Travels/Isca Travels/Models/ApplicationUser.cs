using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Isca_Travels.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Reservation>? reservations { get; set; }
        [Required,MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
    }
}
