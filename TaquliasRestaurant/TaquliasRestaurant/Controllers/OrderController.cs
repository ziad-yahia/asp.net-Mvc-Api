using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaquliasRestaurant.Data;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.Models;
using TaquliasRestaurant.ViewModel;

namespace TaquliasRestaurant.Controllers
{
    public class OrderController : Controller
    {

        private readonly IproductInterface product;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderInterface order;
        private readonly ApplicationDbContext context;

        public OrderController(IproductInterface product, UserManager<ApplicationUser> userManager, IOrderInterface order, ApplicationDbContext context)
        {

            this.product = product;
            this.userManager = userManager;
            this.order = order;
            this.context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.get<OrderVm>("OrderVm") ?? new OrderVm
            {
                OrderItems = new List<OrderItemVm>(),
                Products = await product.GetAllAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(int ProductQunt, int ProductId)
        {
            var products = await context.Products.FindAsync(ProductId);
            // var products= await  product.GetByIdAsync(ProductId);
            if (products == null)
            {
                return NotFound();
            }

            var model = HttpContext.Session.get<OrderVm>("OrderVm") ?? new OrderVm
            {
                OrderItems = new List<OrderItemVm>(),
                Products = await product.GetAllAsync()
            };

            var existitem = model.OrderItems?.FirstOrDefault(x => x.ProductId == ProductId);
            if (existitem != null)
            {
                existitem.Quantity += ProductQunt;
            }
            else
            {
                model.OrderItems.Add(new OrderItemVm
                {
                    Price = products.Price,
                    ProductId = products.ProductId,
                    Quantity = ProductQunt,
                    ProductName = products.Name
                });
            }
            model.TotalAmount = model.OrderItems.Sum(x => x.Quantity * x.Price);

            HttpContext.Session.set("OrderVm", model);

            return RedirectToAction("Create", model);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var model = HttpContext.Session.get<OrderVm>("OrderVm");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.get<OrderVm>("OrderVm");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            Order orders = new Order
            {
                OrderDate=DateTime.Now,
                TotalAmount=model.TotalAmount,
                UserId=userManager.GetUserId(User)
            };

            foreach (var item in model.OrderItems)
            {
                orders.OrderItems.Add(new OrderItem 
                {
                     ProductId= item.ProductId,
                     Quantity=item.Quantity,
                     Price=item.Price
                });
            }
            await order.AddAsync(orders);

            HttpContext.Session.Remove("OrderVm");

            return RedirectToAction("ViewOrders");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewOrders() 
        {
            var userId = userManager.GetUserId(User);
           ViewBag.UserId = userId; 
            return View(await order.GetAllAsync());   
        }
      
    }   
}
