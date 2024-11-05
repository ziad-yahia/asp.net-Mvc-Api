using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TaquliasRestaurant.Data;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Repository
{
    public class IngredintsRepository : IingredentInterface
    {
        private readonly ApplicationDbContext dbContext;

        public IngredintsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(IngredientVM ingredientvm)
        {
            if (ingredientvm == null) return;
           Ingredient ingredient =new Ingredient() { Name = ingredientvm.Name };
           await dbContext.Ingredients.AddAsync(ingredient);
           await dbContext.SaveChangesAsync();
          
        }

        public async Task DeleteAsync(int? id)
        {
            if(id == null)
                return;
             dbContext.Remove(id);
            await dbContext.SaveChangesAsync();
           
        }

        public async Task<ICollection<Ingredient>> GetAllAsync()
        {
            return await dbContext.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int? id)
        {
            var All = await dbContext.Ingredients.Include(i => i.ProductIngredients).ThenInclude(p => p.Product).FirstOrDefaultAsync(i => i.IngredientId == id);
            if (All == null)
                return null;
            return All;
        }

        public async Task UpdateAsync(int? id,IngredientVM ingredientvm)
        {
            if (ingredientvm == null) return;
            Ingredient newingredient = new Ingredient() { Name = ingredientvm.Name };

            var oldingredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.IngredientId == id);
            if (oldingredient == null) return;

            oldingredient.Name = newingredient.Name;
            dbContext.Ingredients.Update(oldingredient);
            await dbContext.SaveChangesAsync();
          
        }
    }
}
