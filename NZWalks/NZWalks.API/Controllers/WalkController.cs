using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walks;
using NZWalks.API.Interfaces;
using NZWalks.API.Models.Mappers;
using Microsoft.AspNetCore.Authorization;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalkController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IWalkRepository repository;
        public WalkController(NZWalksDbContext _dbContext, IWalkRepository _repository)
        {
            dbContext = _dbContext;
            repository = _repository;
        }

        [HttpGet]
        public async Task<ActionResult> Getall() {
            //Data Form dataBase
            var allwalks =await repository.GetallAsync();
            //Data To Dto
            var walkDto = allwalks.Select(s => s.ToWalksDto());
            return Ok(walkDto);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> Getbyid([FromRoute]Guid id)
        {
            //from database
            var walk = await repository.GetByIdAsync(id);
            if (walk == null)
            {
                return BadRequest(404);
            }
            // data to dto
            var walkDto = walk.ToWalksDto();

           
            return Ok(walkDto); 
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddWalkRequestDto addWalkRequest) 
        {
            // data from dtorequest to database             
            var walk= await  repository.CreateAsync(addWalkRequest);
#region old create code
            /*
                       //datafrom database to dto
                       var walkdto =new WalkDto();
                       walkdto.Id = walk.Id;
                       walkdto.Name = walk.Name;
                       walkdto.Description = walk.Description;
                       walkdto.LengthInKm = addWalkRequest.LengthInKm;
                       walkdto.WalkImageUrl = walk.WalkImageUrl;
                       walkdto.DifficultyId = walk.DifficultyId;
                       walkdto.RegionId = walk.RegionId;
                       var reg = dbContext.Regions.Where(r => r.Id == walk.RegionId).ToList();
                       foreach (var item in reg) 
                       {   
                           walkdto.RegionName = item.Name;
                       }
           */
            #endregion

            var walkDto = walk.UpdateCreateWalkDto();
            return CreatedAtAction(nameof(Getbyid),new {id= walkDto.Id}, walkDto);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody]  AddWalkRequestDto addWalkRequest)
        {     
            var walkDomain =await repository.UpdateAsync(id,addWalkRequest);
 
            if (walkDomain == null) 
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
#region old update
              /* 
                WalkDto walkDto = new WalkDto();
                walkDto.Id = walkDomain.Id;
                walkDto.Name = walkDomain.Name;
                walkDto.Description = walkDomain.Description;
                walkDto.DifficultyId = walkDomain.DifficultyId;
                walkDto.RegionId = walkDomain.RegionId;
                walkDto.LengthInKm = walkDomain.LengthInKm;
                walkDto.WalkImageUrl = walkDomain.WalkImageUrl;
              */
                #endregion
                var walkDto = walkDomain.UpdateCreateWalkDto();
                return CreatedAtAction(nameof(Getbyid), new { id = walkDto.Id }, walkDto);         
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {

            var walkdomain = await repository.DeleteAsync(id);

            if (walkdomain == null)
            {
                return BadRequest(404);
            }

            return NoContent();

        }
    }
}
