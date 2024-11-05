using Isca_Travels.Areas.Admin.Models;
using Isca_Travels.Models;
using Isca_Travels.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Isca_Travels.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> trips { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<ReservationDetails> reservationDetails { get; set; }
        public DbSet<ConfirmedReservation> ConfirmedReservation { get; set; }

        


    }
}
