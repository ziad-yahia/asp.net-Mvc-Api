using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Interfaces;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walks;
using NZWalks.API.Models.Mappers;
namespace NZWalks.API.Services
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkRepository(NZWalksDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public Task<List<Walk>> GetallAsync()
        {
           //Data Form dataBase
           return dbContext.Walks.Include(r => r.Region).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include(x => x.Region).FirstAsync(x => x.Id == id);                                
        }
        public async Task<Walk> CreateAsync(AddWalkRequestDto addWalkRequest)
        {
            var walk = new Walk();
            walk.Name = addWalkRequest.Name;
            walk.Description = addWalkRequest.Description;
            walk.LengthInKm = addWalkRequest.LengthInKm;
            walk.WalkImageUrl = addWalkRequest.WalkImageUrl;
            walk.DifficultyId = addWalkRequest.DifficultyId;
            walk.RegionId = addWalkRequest.RegionId;
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
        public async Task<Walk> UpdateAsync(Guid id, AddWalkRequestDto addWalkRequest)
        {

            var walkDomain =await dbContext.Walks.FindAsync(id);

            if (walkDomain == null) 
            {
               return null;
            }
                walkDomain.Name = addWalkRequest.Name;
                walkDomain.Description = addWalkRequest.Description;
                walkDomain.DifficultyId = addWalkRequest.DifficultyId;
                walkDomain.RegionId = addWalkRequest.RegionId;
                walkDomain.LengthInKm = addWalkRequest.LengthInKm;
                walkDomain.WalkImageUrl = addWalkRequest.WalkImageUrl;
                
                dbContext.Walks.Update(walkDomain);
                await dbContext.SaveChangesAsync();

            return walkDomain;
            }
        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walkdomain = await dbContext.Walks.FindAsync(id);

            if (walkdomain == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walkdomain);
            await dbContext.SaveChangesAsync();    
            return walkdomain;
        }

        
    }
}
