namespace TaquliasRestaurant.ViewModel
{
    public class ProductVm
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int CategoryId { get; set; }
        public int[]? ingredentId { get; set; }

    }
}
