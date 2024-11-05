using System.ComponentModel.DataAnnotations.Schema;

namespace Isca_Travels.Areas.Admin.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public string? ImgPath { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? Notes { get; set; }
    }
}
