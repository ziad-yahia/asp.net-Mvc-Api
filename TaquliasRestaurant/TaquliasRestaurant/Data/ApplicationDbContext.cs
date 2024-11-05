﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaquliasRestaurant.Models;

namespace TaquliasRestaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

          /*  builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { UserName="ziad",Email="Ziad@gmail.com",PasswordHash="123456Ziad***", }
                );
          */

            builder.Entity<ProductIngredient>().
                HasKey(pi => new { pi.ProductId,pi.IngredientId } );

            builder.Entity<ProductIngredient>()
                .HasOne(Pi => Pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            builder.Entity<ProductIngredient>().
                HasOne(pi => pi.ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi=>pi.IngredientId);

            //Data seed
            builder.Entity<Category>().HasData(
                new Category { CategoryId=1,Name="Appetizer"},
                new Category { CategoryId = 2, Name = "Entree" },
                new Category { CategoryId = 3, Name = "Side Dish" },
                new Category { CategoryId = 4, Name = "Dessert" },
                new Category { CategoryId = 5, Name = "Beverage" }
            );

            builder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Beef" },
                new Ingredient { IngredientId = 2, Name = "Chicken" },
                new Ingredient { IngredientId = 3, Name = "Fish" },
                new Ingredient { IngredientId = 4, Name = "TorTilla" },
                new Ingredient { IngredientId = 5, Name = "Lettuce" },
                new Ingredient { IngredientId = 6, Name = "Tomato"}
            );
            builder.Entity<Product>().HasData(
               new Product
               {
                   ProductId = 1,
                   Name = "Beef Taco",
                   Description = "A Delicious Beef Taco",
                   Price = 200,
                   Stock = 100,
                   CategoryId = 2
               },
                new Product
                {
                    ProductId = 2,
                    Name = "Chicken Taco",
                    Description = "A Delicious Chicken Taco",
                    Price = 150,
                    Stock = 101,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Fish Taco",
                    Description = "A Delicious Taco",
                    Price = 100,
                    Stock = 90,
                    CategoryId = 2
                }
               
        );
        builder.Entity<ProductIngredient>().HasData(
           new ProductIngredient { ProductId = 1, IngredientId = 1 },
           new ProductIngredient { ProductId = 1, IngredientId = 4 },
           new ProductIngredient { ProductId = 1, IngredientId = 5 },
           new ProductIngredient { ProductId = 1, IngredientId = 6 },
           new ProductIngredient { ProductId = 2, IngredientId = 2 },
           new ProductIngredient { ProductId = 2, IngredientId = 4 },
           new ProductIngredient { ProductId = 2, IngredientId = 5 },
           new ProductIngredient { ProductId = 2, IngredientId = 6 },
           new ProductIngredient { ProductId = 3, IngredientId = 3 },
           new ProductIngredient { ProductId = 3, IngredientId = 4 },
           new ProductIngredient { ProductId = 3, IngredientId = 5 },
           new ProductIngredient { ProductId = 3, IngredientId = 6 }
        );
        }
    }
}
