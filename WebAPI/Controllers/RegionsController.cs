using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
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

        [HttpGet]
        public IActionResult GetAll()
        {

            var regionsDB = dbContext.Regions.ToList();

            // mapping to a DTO
            var regionsDto = new List<RegionDto>();

            if (regionsDB.Count == 0)
            {
                return NotFound("No regions found.");
            }

            foreach (var region in regionsDB)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name
                });
            }

            return Ok(regionsDto);

        }

        // Get by Id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var region = dbContext.Regions.Find(id);


            if (region is null)
            {
                return NotFound("Region not found.");
            }

            // Mappint to Dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name
            };

            return Ok(regionDto);

        }

        // Create
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Mappint Dto to DomainModel
            var region = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,

            };

            // Db Context to create new region
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            // mapping domain to dto to return it
            var regionDto = dbContext.Regions.Find(region.Id);

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDto);

        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var region = dbContext.Regions.Find(id);

            if (region is null)
            {
                return NotFound("Region not found.");
            }

            // Mapping Dto to Domain
            region.Code = updateRegionRequestDto.Code;
            region.Name = updateRegionRequestDto.Name;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            return NoContent();

        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {

            var region = dbContext.Regions.Find(id);

            if (region is null)
            {
                return NotFound("Region not found.");
            }

            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            return NoContent();

        }

    }
}
