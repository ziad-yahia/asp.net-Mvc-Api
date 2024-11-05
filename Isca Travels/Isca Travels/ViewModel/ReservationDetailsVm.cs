using Isca_Travels.Models;

namespace Isca_Travels.ViewModel
{
    public class ReservationDetailsVm
    {
        public string? TripName { get; set; }
        public string? UserName { get; set; }
        public int TripId { get; set; }
        public int adultCount { get; set; }
        public int ChildCount { get; set; }
        public string Phonenumber { get; set; }
        public DateTime StartDate { get; set; }
        public bool PrivateTrip { get; set; }
        public decimal? Price { get; set; }
    }
}