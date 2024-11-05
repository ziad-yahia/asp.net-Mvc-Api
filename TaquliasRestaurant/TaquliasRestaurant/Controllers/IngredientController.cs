using Microsoft.AspNetCore.Mvc;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IingredentInterface ingredent;

        public IngredientController(IingredentInterface Iingredent)
        {
            this.ingredent = Iingredent;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await ingredent.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            return View(await ingredent.GetByIdAsync(id));
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientVM ingredientVM)
        {
            if (ingredientVM != null)
            {
                await ingredent.AddAsync(ingredientVM);
                return RedirectToAction("Index");
            }
            return View(ingredientVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await ingredent.GetByIdAsync(id));             
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id ,IngredientVM ingredientVM)
        {
            if (ingredientVM != null && id != null)
            {
               await ingredent.UpdateAsync(id,ingredientVM);
                return RedirectToAction("Index");
            }
            return View(ingredientVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await ingredent.DeleteAsync(id);    
            return RedirectToAction("Index");
        }
    }
}
