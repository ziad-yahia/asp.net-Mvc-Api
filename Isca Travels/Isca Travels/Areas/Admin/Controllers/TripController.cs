using Isca_Travels.Areas.Admin.Models;
using Isca_Travels.Areas.Admin.ViewModel;
using Isca_Travels.AutoMapper;
using Isca_Travels.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Isca_Travels.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TripController : Controller
    {
        private readonly InterfaceService<Trip> interfaceServices;

        public TripController(InterfaceService<Trip> interfaceServices)
        {
            this.interfaceServices = interfaceServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await interfaceServices.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trip = await interfaceServices.GetByIdAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripVm trip)
        {
            if (trip == null) NotFound();
            
        
            var trips=trip.MappingToDomain();
            if (ModelState.IsValid)
            {
                await interfaceServices.CreateAsync(trips);
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
           if(id == null)NotFound();
            Trip trip = await interfaceServices.GetByIdAsync(id);
            //send to view
            UpdateTripVm trips = new UpdateTripVm();
            trips.Name = trip.Name;
            trips.Description = trip.Description;
            trips.Price = trip.Price;
            trips.Days = trip.Days;
            trips.ImgPath = trip.ImgPath;
            trips.Notes = trip.Notes;
            if (trips == null) NotFound();

            return View(trips);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripVm trip)
        {
            if (ModelState.IsValid)
            {
                Trip trips = new Trip();
                trips.Name = trip.Name;
                trips.Description = trip.Description;
                trips.Price = trip.Price;
                trips.Days = trip.Days;
                trips.ImageFile = trip.ImageFile;
                trips.Notes = trip.Notes;

                await interfaceServices.UpdateAsync(id, trips);
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trip = await interfaceServices.DeleteAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
