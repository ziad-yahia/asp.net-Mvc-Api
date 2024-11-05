using Isca_Travels.Areas.Admin.Models;
using Isca_Travels.Models;

namespace Isca_Travels.ViewModel
{
    public class ReservationVm
    {
        public decimal TotalAmount { get; set; }
        public List<ReservationDetailsVm>? ReservationDetailsVm { get; set;}
        public IEnumerable<Trip>? trips { get; set; }
    }
}
