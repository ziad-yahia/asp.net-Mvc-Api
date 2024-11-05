using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: IdentityDbContext
    {
        public NZWalksDbContext(DbContextOptions options):base(options)
        {
           
        }

        public DbSet<Difficulty> difficulties { get; set; } 
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }  
    }
}
