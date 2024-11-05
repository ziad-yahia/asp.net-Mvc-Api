using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public DifficultyController(NZWalksDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var defficulty = dbContext.difficulties.ToList();
            return Ok(defficulty);
        }

        [HttpPost]
        public IActionResult Create(Difficulty difficulty)
        {
            var difficl = new Difficulty();
            difficl.Id = difficulty.Id;
            difficl.Name = difficulty.Name;

            dbContext.difficulties.Add(difficl);
            dbContext.SaveChanges();
            return Created(nameof(Getall), difficl);
        }
    }
}
