using Isca_Travels.Areas.Admin.Models;

namespace Isca_Travels.Models
{
    public class ConfirmedReservation
    {
        public int Id { get; set; }
        public string TripName { get; set; }
        public string UserName { get; set; }
        public int adultCount { get; set; }
        public int ChildCount { get; set; }
        public string Phonenumber { get; set; }
        public DateTime StartDate { get; set; }
        public bool PrivateTrip { get; set; }
        public decimal? Price { get; set; }
    }
}
