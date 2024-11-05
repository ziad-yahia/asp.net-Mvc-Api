using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Interface
{
    public interface IcategoryInterface
    {
        Task<ICollection<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int? id);
        Task AddAsync(CategoryVm category);
        Task UpdateAsync(int? id, CategoryVm category);
        Task DeleteAsync(int? id);
    }
}
