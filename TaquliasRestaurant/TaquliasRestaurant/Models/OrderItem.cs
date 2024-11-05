﻿namespace TaquliasRestaurant.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrerId { get; set; }
        public Order? Order { get;set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
