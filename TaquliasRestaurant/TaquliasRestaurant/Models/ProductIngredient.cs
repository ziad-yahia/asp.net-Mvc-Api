namespace TaquliasRestaurant.Models
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int IngredientId { get; set; }
        public Ingredient? ingredient  { get;set; }
}
}