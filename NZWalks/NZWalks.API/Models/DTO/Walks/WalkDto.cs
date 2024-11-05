using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTO.Walks
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
        public string RegionName { get; set; } = null;

    }
}
