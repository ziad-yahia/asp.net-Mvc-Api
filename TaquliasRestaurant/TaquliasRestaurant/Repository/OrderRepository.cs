using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaquliasRestaurant.Data;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Repository
{
    public class OrderRepository : IOrderInterface
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Order Order)
        {
             await dbContext.Orders.AddAsync(Order);
            await dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Order>> GetAllAsync()
        {
            
            return await dbContext.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(string? id)
        {
            // return await dbContext.Orders.Include(x =>x.OrderItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == id);
             throw new NotImplementedException();
        }

        public Task UpdateAsync(int? id, OrderVm Order)
        {
            throw new NotImplementedException();
        }
    }
}
