using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaquliasRestaurant.Data;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Repository
{
    public class ProductRepository:IproductInterface
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductRepository(ApplicationDbContext dbContext,IWebHostEnvironment _webHostEnvironment)
        {
            this.dbContext = dbContext;
            webHostEnvironment = _webHostEnvironment;
        }

        public async Task AddAsync(ProductVm ProductVm)
        {
          //  if (ProductVm == null) throw new ArgumentNullException(nameof(ProductVm));   
             Product product = new Product() 
             {
                 Name=ProductVm.Name,
                 Description=ProductVm.Description,
                 Price=ProductVm.Price,
                 Stock= ProductVm.Stock,
                 CategoryId=ProductVm.CategoryId
             };
            if (ProductVm.ImageFile != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string uniquefilename = Guid.NewGuid().ToString() + "" + ProductVm.ImageFile.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await ProductVm.ImageFile.CopyToAsync(filestream);
                }
                product.ImageUrl=uniquefilename;
            }
            foreach (int item in ProductVm.ingredentId) 
            {
                product.ProductIngredients?.Add( new ProductIngredient { ProductId= product.ProductId , IngredientId= item });
            }
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var product= await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
               await dbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return  await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            if (id == null)
                return null;
            return await dbContext.Products.Include(i => i.ProductIngredients).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task UpdateAsync(int? id, ProductVm ProductVm)
        {
           var oldproduct= await dbContext.Products.Include(i => i.ProductIngredients).FirstOrDefaultAsync(x => x.ProductId == id);
            if (oldproduct == null) return ;
            if (ProductVm == null) return;
            oldproduct.Name = ProductVm.Name;
            oldproduct.Description = ProductVm.Description;
            oldproduct.Price = ProductVm.Price;
            oldproduct.Stock = ProductVm.Stock;
            oldproduct.CategoryId = ProductVm.CategoryId;
            //image check
            if (ProductVm.ImageFile != null || oldproduct.ImageFile != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string uniquefilename = Guid.NewGuid().ToString() + "" + ProductVm.ImageFile?.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await ProductVm.ImageFile.CopyToAsync(filestream);
                }
                oldproduct.ImageUrl = uniquefilename;
            }
            oldproduct.ProductIngredients?.Clear();
            foreach (var item in ProductVm.ingredentId)
            { 
                    oldproduct.ProductIngredients?.Add(new ProductIngredient{ProductId = oldproduct.ProductId, IngredientId = item });    
            }
             dbContext.Products.Update(oldproduct);
            await dbContext.SaveChangesAsync();
        }
    }
}

