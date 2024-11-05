using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [ForeignKey("Difficulty")]
        public Guid DifficultyId { get; set; }
        [ForeignKey("Region")]
        public Guid RegionId { get; set; }

        //Navigation Properities
        public virtual Difficulty Difficulty { get; set; }
        public virtual Region Region { get; set; }  
    }
}
