using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Interface
{
    public interface IingredentInterface
    {
        Task<ICollection<Ingredient>> GetAllAsync();
        Task<Ingredient> GetByIdAsync(int? id);
        Task AddAsync(IngredientVM ingredient);
        Task UpdateAsync(int? id,IngredientVM ingredient);
        Task DeleteAsync(int? id);
    }
}
