using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walks;

namespace NZWalks.API.Models.Mappers
{
    public static class WalkMapperDto
    {
        public static WalkDto ToWalksDto(this Walk walk)
        {

            return new WalkDto
            {
                Id = walk.Id,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                WalkImageUrl = walk.WalkImageUrl,
                DifficultyId = walk.DifficultyId,
                RegionId = walk.RegionId,
                RegionName = walk.Region.Name
            };
        }

        public static WalkDto UpdateCreateWalkDto(this Walk walk)
        {

            return new WalkDto
            {
                Id = walk.Id,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                WalkImageUrl = walk.WalkImageUrl,
                DifficultyId = walk.DifficultyId,
                RegionId = walk.RegionId,
            };
        }
    }
}
