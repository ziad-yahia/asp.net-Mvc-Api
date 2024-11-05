using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isca_Travels.Areas.Admin.ViewModel
{
    public class UpdateTripVm
    {
    
        [Required(ErrorMessage = "Trip Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Trip Description Is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Trip Days Cant Be Empty")]
        public int Days { get; set; }
        [Required(ErrorMessage = "Trip Price Cant Be Empty")]
        public decimal Price { get; set; }
        public string? ImgPath { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Notes { get; set; }
    }
}
