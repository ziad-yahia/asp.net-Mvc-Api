using Microsoft.EntityFrameworkCore;
using TaquliasRestaurant.Data;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Repository
{
    public class CategoryRepository:IcategoryInterface
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddAsync(CategoryVm category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
           return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
            return await dbContext.Categories.FindAsync(id);
        }

        public Task UpdateAsync(int? id, CategoryVm category)
        {
            throw new NotImplementedException();
        }
    }
}
