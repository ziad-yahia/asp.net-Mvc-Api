using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Interface
{
    public interface IproductInterface
    {
        Task<ICollection<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
        Task AddAsync(ProductVm ingredient);
        Task UpdateAsync(int? id, ProductVm ingredient);
        Task DeleteAsync(int? id);
    }
}
