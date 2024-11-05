using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaquliasRestaurant.Models
{
    public class Product
    {
        public Product()
        {
            ProductIngredients=new List<ProductIngredient>();
        }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category? category { get; set; }  // Product belongs to category
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; } // product can be in many items
        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients {  get; set; }



    }
}