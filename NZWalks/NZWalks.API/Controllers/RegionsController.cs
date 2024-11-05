using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Regions;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region all Regions
        //get method 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
 

            //Data form databse 
            var regionsDomain = await dbContext.Regions.Include(w => w.Walk).ToListAsync();

              //mapping Data To DTOs
              var RegionDto = new List<RegionDto>();

              foreach (var item in regionsDomain)
              {
                  RegionDto.Add(
                     new RegionDto() {
                          Id = item.Id,
                          Name = item.Name,
                          Code = item.Code,
                          RegionImageUrl = item.RegionImageUrl,
                        

                     }
                  );
              }

            
            return Ok(RegionDto);
        }
        #endregion

        //get By Id method
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            //Data form Database
            var regiondomain = dbContext.Regions.Include(w=>w.Walk).FirstOrDefault(r=>r.Id==id);

            if(regiondomain == null)
            {
                  return NotFound();
            }

            //Mapping Data To DTO
            var RegionDto = new RegionByIdDto();
            RegionDto.Id = regiondomain.Id;
            RegionDto.Name = regiondomain.Name;
            RegionDto.Code = regiondomain.Code;
            RegionDto.RegionImageUrl = regiondomain.RegionImageUrl;

            foreach (var item in regiondomain.Walk)
            {
                if (item.RegionId == regiondomain.Id)
                {
                    RegionDto.WalkName.Add(item.Name);
                }
                else
                {
                    RegionDto.WalkName.Add(null);
                }
            }
           
            return Ok(RegionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]AddRegionRequestDto regionrequest)
        {
            //mapp Dto class To Domain class  
            var regiondomain = new Region();
            regiondomain.Name = regionrequest.Name;
            regiondomain.Code = regionrequest.Code;
            regiondomain.RegionImageUrl = regionrequest.RegionImageUrl; 
            dbContext.Regions.Add(regiondomain);
            dbContext.SaveChanges();

            // mapp domain class to dto similar class
            var regionDto = new RegionDto();
            regionDto.Id = regiondomain.Id;
            regionDto.Code = regiondomain.Code;
            regionDto.RegionImageUrl = regiondomain.RegionImageUrl;
            regionDto.Name = regiondomain.Name;
          
          


            return CreatedAtAction(nameof(GetById),new { id=regionDto.Id},regionDto);
        }
        [HttpPut("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody]AddRegionRequestDto addRegionRequestDto)
        { 
            var regiondomain = dbContext.Regions.Find(id);
            if (regiondomain != null)
            {
                regiondomain.Name = addRegionRequestDto.Name;
                regiondomain.Code = addRegionRequestDto.Code;
                regiondomain.RegionImageUrl = addRegionRequestDto.RegionImageUrl;
                dbContext.Regions.Update(regiondomain);
                dbContext.SaveChanges();

                var regiondto = new RegionDto 
                { 
                    Id = regiondomain.Id,
                    Name = regiondomain.Name,
                    Code = regiondomain.Code,
                    RegionImageUrl = regiondomain.RegionImageUrl,

                };
                return CreatedAtAction(nameof(GetById),new { id = regiondto.Id }, regiondto );
            }
           return BadRequest(404);
        }
        [HttpDelete("{id:Guid}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            try {
                var regiondomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
                dbContext.Regions.Remove(regiondomain);
                dbContext.SaveChanges();
                return Ok();
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
