using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models.Domain;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        public WalkController(NZWalksDbContext dbContext)
        {

            this.dbContext = dbContext;

        }

        // Get all walks
        [HttpGet]
        public IActionResult GetAll()
        {

            var walksDB = dbContext.Walks.ToList();

            if (walksDB.Count == 0)
            {
                return NotFound("No walks found.");
            }

            return Ok(walksDB);

        }

        // Get by Id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var walk = dbContext.Walks.Find(id);

            if (walk == null)
            {
                return NotFound("Walk not found.");
            }

            return Ok(walk);

        }

        // Get walks by region
        [HttpGet]
        [Route("region/{regionId}")]
        public IActionResult GetByRegion([FromRoute] Guid regionId)
        {

            var walksDB = dbContext.Walks.Where(w => w.RegionId == regionId).ToList();

            if (walksDB.Count == 0)
            {
                return NotFound("No walks found for this region.");
            }

            return Ok(walksDB);

        }

        // Get walks by difficulty
        [HttpGet]
        [Route("difficulty/{difficulty}")]
        public IActionResult GetByDifficulty([FromRoute] string difficulty)
        {

            var walksDB = dbContext.Walks.Where(w => w.Difficulty.Name == difficulty).ToList();

            if (walksDB.Count == 0)
            {
                return NotFound("No walks found for this difficulty.");
            }

            return Ok(walksDB);

        }

        // Get walks by region and difficulty
        [HttpGet]
        [Route("region/{regionId}/difficulty/{difficulty}")]
        public IActionResult GetByRegionAndDifficulty([FromRoute] Guid regionId, [FromRoute] string difficulty)
        {

            var walksDB = dbContext.Walks.Where(w => w.RegionId == regionId && w.Difficulty.Name == difficulty).ToList();

            if (walksDB.Count == 0)
            {
                return NotFound("No walks found for this region and difficulty.");
            }

            return Ok(walksDB);

        }

        // POST: api/Walk
        [HttpPost]
        public IActionResult Create([FromBody] Walk walk)
        {

            dbContext.Walks.Add(walk);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, walk);

        }

        // PUT: api/Walk/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Walk walk)
        {

            var walkDB = dbContext.Walks.Find(id);

            if (walkDB == null)
            {
                return NotFound("Walk not found.");
            }

            walkDB.Name = walk.Name;
            walkDB.Description = walk.Description;
            walkDB.LenghtInKM = walk.LenghtInKM;
            walkDB.WalkImageUrl = walk.WalkImageUrl;
            walkDB.DifficultyId = walk.DifficultyId;
            walkDB.RegionId = walk.RegionId;

            dbContext.SaveChanges();

            return NoContent();

        }

        // DELETE: api/Walk/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {

            var walk = dbContext.Walks.Find(id);

            if (walk == null)
            {
                return NotFound("Walk not found.");
            }

            dbContext.Walks.Remove(walk);
            dbContext.SaveChanges();

            return NoContent();

        }

    }
}
