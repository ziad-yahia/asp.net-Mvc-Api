using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walks;

namespace NZWalks.API.Interfaces
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetallAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(AddWalkRequestDto AddWalkRequest);
        Task<Walk?> UpdateAsync(Guid id, AddWalkRequestDto AddWalkRequest);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
