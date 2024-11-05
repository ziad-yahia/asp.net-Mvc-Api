using Isca_Travels.Data;
using Isca_Travels.Interface;
using Isca_Travels.Models;
using Isca_Travels.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Isca_Travels.Repository
{
    public class ConfirmationRepostiory : InterfaceService<ConfirmedReservation>
    {
        private readonly ApplicationDbContext context;

        public ConfirmationRepostiory(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ConfirmedReservation> CreateAsync(ConfirmedReservation anyclass)
        {
           
             await  context.ConfirmedReservation.AddAsync(anyclass);
            await  context.SaveChangesAsync();
            return anyclass;
        }

        public Task<ConfirmedReservation?> DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ConfirmedReservation>> GetAllAsync()
        {
            return  await context.ConfirmedReservation.ToListAsync();
        }

        public async Task<ConfirmedReservation> GetByIdAsync(int? id)
        {
            return await context.ConfirmedReservation.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<ConfirmedReservation?> UpdateAsync(int? id, ConfirmedReservation anyclass)
        {
            throw new NotImplementedException();
        }
    }
}
