using Microsoft.AspNetCore.Mvc;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Controllers
{
    public class ProductController : Controller
    {
        private readonly IproductInterface iproductInterface;
        private readonly IingredentInterface ingredent;
        private readonly IcategoryInterface category;

        public ProductController(IproductInterface iproductInterface,IingredentInterface ingredent,IcategoryInterface category)
        {
            this.iproductInterface = iproductInterface;
            this.ingredent = ingredent;
            this.category = category;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await iproductInterface.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details (int id)
        {
            return View(await iproductInterface.GetByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.Ingredeint= await ingredent.GetAllAsync();
            ViewBag.category = await  category.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductVm productVm)
        {
           
            if (productVm != null)
            {
                await iproductInterface.AddAsync(productVm);
                return RedirectToAction("Index");
            }
           
            return View(productVm);
          
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Ingredeint = await ingredent.GetAllAsync();
            ViewBag.category = await category.GetAllAsync();         
            return View(await iproductInterface.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int?id ,ProductVm product)
        {
            ViewBag.Ingredeint = await ingredent.GetAllAsync();
            ViewBag.category = await category.GetAllAsync();
            if (product != null && id != null) 
            {
              await  iproductInterface.UpdateAsync(id,product);
              return RedirectToAction("Index");
            }

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
           await iproductInterface.DeleteAsync(id);
            return RedirectToAction("Index");
        }


    }
}
