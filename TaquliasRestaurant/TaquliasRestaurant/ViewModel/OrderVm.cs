using TaquliasRestaurant.Models;

namespace TaquliasRestaurant.ViewModel
{
    public class OrderVm
    {
        public decimal TotalAmount { get; set; }
        public List<OrderItemVm>? OrderItems { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
