namespace NZWalks.API.Models.DTO.Regions
{
    public class RegionByIdDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
        public List<string> WalkName { get; set; } = new List<string>();
    }
}
