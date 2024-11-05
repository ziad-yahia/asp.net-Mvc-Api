using Isca_Travels.Areas.Admin.Models;
using Isca_Travels.Data;
using Isca_Travels.Interface;
using Isca_Travels.Models;
using Isca_Travels.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Isca_Travels.Controllers
{
    public class ReservationController : Controller
    {
        private readonly InterfaceService<Trip> service;
        private readonly InterfaceService<ConfirmedReservation> confitmation;
        //  private readonly Reservation reservation;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> user;

        public ReservationController(InterfaceService<Trip> service, ApplicationDbContext dbContext, UserManager<ApplicationUser> user, InterfaceService<ConfirmedReservation> confitmation)
        {
            this.service = service;
            this.dbContext = dbContext;
            this.user = user;
            this.confitmation = confitmation;
        }
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.GetTS<ReservationVm>("ReservationVm") ?? new ReservationVm
            {
                ReservationDetailsVm = new List<ReservationDetailsVm>(),
                trips = await service.GetAllAsync()
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var trip = await service.GetByIdAsync(id);
            return View(trip);
        }
        
        [HttpGet("Reserve/{id?}")]
        
        public async Task<IActionResult> ReservationDetails(int? id)
        {
             var trip = await service.GetByIdAsync(id);
            if(trip == null)return NotFound();
                return View(trip);
            
        }
        [HttpGet]
        public  IActionResult RemoveTrip(int id)
        {
            var cartitems = HttpContext.Session.GetTS<ReservationVm>("ReservationVm");
            var existcartitem= cartitems.ReservationDetailsVm.FirstOrDefault(x => x.TripId ==id);
            if (existcartitem != null)
            {
                cartitems.ReservationDetailsVm.Remove(existcartitem);
                // update the price 
                decimal ChildDiscount = 0.5m;
                cartitems.TotalAmount = (decimal)(cartitems.ReservationDetailsVm.Sum(i => i.Price * i.adultCount)
                + cartitems.ReservationDetailsVm.Sum(i => i.ChildCount * i.Price * ChildDiscount));
            }
            HttpContext.Session.SetS( "ReservationVm", cartitems);
            return RedirectToAction("Cart",cartitems);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrip(int Tripid, int Adult, int Child,bool isprivate,DateTime Datestart,string UserName,string Phonenumber)
        {
            var trip = await service.GetByIdAsync(Tripid);
            if (trip == null) NotFound();

            var model = HttpContext.Session.GetTS<ReservationVm>("ReservationVm") ?? new ReservationVm
            {
                ReservationDetailsVm = new List<ReservationDetailsVm>(),
                trips = await service.GetAllAsync()
            };
            var alreadyreserve = model.ReservationDetailsVm?.FirstOrDefault(x => x.TripId == Tripid);
            //need to handel if reservation  already found 
            if (alreadyreserve == null) 
            {
                model.ReservationDetailsVm?.Add(new ReservationDetailsVm {
                    TripId = trip.Id,
                    adultCount=Adult,
                    ChildCount=Child,
                    TripName=trip?.Name,
                     StartDate=Datestart,
                    PrivateTrip=isprivate,
                    Price= trip?.Price,
                    UserName=UserName,
                    Phonenumber= Phonenumber
                    //need to add if private or not
                });
            }
            decimal ChildDiscount=0.5m;
            model.TotalAmount = (decimal)(model.ReservationDetailsVm?.Sum(i => i.Price * i.adultCount)
            + model.ReservationDetailsVm.Sum(i => i.ChildCount * i.Price * ChildDiscount ));

            HttpContext.Session.SetS("ReservationVm",model );
            return RedirectToAction("Create",model);
        }
        public IActionResult Cart()
        {
            var reservation =  HttpContext.Session.GetTS<ReservationVm>("ReservationVm");
            if (reservation == null || reservation.ReservationDetailsVm.Count == 0)
            {
                return   RedirectToAction("Create");
            }
            return View(reservation);
        }

        /*
         * make confirmation
         * */
        [HttpPost]
        public async Task<IActionResult> Confirmation() 
        {
           
             ConfirmedReservation confirmeds = new ConfirmedReservation();
            var model = HttpContext.Session.GetTS<ReservationVm>("ReservationVm") ?? new ReservationVm();
            foreach (var item in model.ReservationDetailsVm)
            {

                confirmeds.adultCount = item.adultCount;
                confirmeds.ChildCount = item.ChildCount;
                confirmeds.Price = item.Price;
                confirmeds.PrivateTrip = item.PrivateTrip;
                confirmeds.StartDate = item.StartDate;
                confirmeds.TripName = item.TripName;
                confirmeds.UserName = item.UserName;
                confirmeds.Phonenumber = item.Phonenumber;
                 
                
                await confitmation.CreateAsync(confirmeds);
                HttpContext.Session.Remove("ReservationVm");
                return RedirectToAction("MyTripReservation");
            }

            return RedirectToAction("Create");
        }
        public async Task<IActionResult> MyTripReservation()
        {
            ViewBag.name = user.GetUserName(User);
            var all=await confitmation.GetAllAsync();
            return View(all);
        }


    }
}
