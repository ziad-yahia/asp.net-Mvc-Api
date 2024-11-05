using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Interface
{
    public interface IOrderInterface
    {
        Task<ICollection<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string? id);
        Task AddAsync(Order Order);
        Task UpdateAsync(int? id, OrderVm Order);
        Task DeleteAsync(int? id);
    }
}
