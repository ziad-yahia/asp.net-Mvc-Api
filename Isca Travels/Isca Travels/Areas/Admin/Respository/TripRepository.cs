using Isca_Travels.Areas.Admin.Models;
using Isca_Travels.Areas.Admin.ViewModel;
using Isca_Travels.Data;
using Isca_Travels.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace Isca_Travels.Areas.Admin.Respository
{
    public class TripRepository:InterfaceService<Trip>
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TripRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<Trip> CreateAsync(Trip trip)
        {            
            if (trip.ImageFile != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                string uniquefilename = Guid.NewGuid().ToString() + "" + trip.ImageFile.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await trip.ImageFile.CopyToAsync(filestream);
                }
                trip.ImgPath = uniquefilename;
            }
             await context.trips.AddAsync(trip);
             await context.SaveChangesAsync();
             return trip;
        }

        public async Task<Trip?> DeleteAsync(int? id)
        {
            var trip = await context.trips.FirstOrDefaultAsync(x => x.Id == id);
            context.trips.Remove(trip);
            await context.SaveChangesAsync();
            return trip;

        }

        public async Task<List<Trip>> GetAllAsync()
        {
            return await context.trips.ToListAsync();
        }

        public async Task<Trip> GetByIdAsync(int? id)
        {
            return await context.trips.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Trip?> UpdateAsync(int? id, Trip anyclass)
        {
            var oldtrip = await context.trips.FirstOrDefaultAsync(x => x.Id == id);
            oldtrip.Notes = anyclass.Notes;
            oldtrip.Days = anyclass.Days;
            oldtrip.Description = anyclass.Description;
            oldtrip.Name = anyclass.Name;
            oldtrip.Price = anyclass.Price;

            if (anyclass.ImageFile != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                string uniquefilename = Guid.NewGuid().ToString() + "" + anyclass.ImageFile.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await anyclass.ImageFile.CopyToAsync(filestream);
                }
                oldtrip.ImgPath = uniquefilename;
            }
            context.trips.Update(oldtrip);
            await context.SaveChangesAsync();
            return oldtrip;
        }
    }
}
