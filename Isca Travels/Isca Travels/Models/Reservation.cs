using Isca_Travels.ViewModel;

namespace Isca_Travels.Models
{
    public class Reservation
    {
        public Reservation()
        {
            ReservationDetails = new List<ReservationDetails>();
        }
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public decimal ? TotalPrice { get; set; }
        public ICollection<ReservationDetails>? ReservationDetails { get; set; }
    }
}