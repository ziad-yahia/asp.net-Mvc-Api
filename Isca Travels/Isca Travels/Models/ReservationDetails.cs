using Isca_Travels.Areas.Admin.Models;

namespace Isca_Travels.Models
{
    public class ReservationDetails
    {
        public int ReservationDetailsId { get; set; }
        public int ReservationId { get; set; }
        public Reservation? reservation { get; set; }
        public int TripId { get; set; }
        public Trip? trip { get; set; }
        public int adultCount { get; set; }
        public int ChildCount { get; set; }
        public bool? PrivateTrip { get; set; }
        public decimal Price { get; set; }
    }
}
