using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        public DifficultyController(NZWalksDbContext dbContext)
        {

            this.dbContext = dbContext;

        }

        // GET: api/Difficulty
        [HttpGet]
        public IActionResult GetAll()
        {

            var Difficulties = dbContext.Difficulties.ToList();

            var DifficultiesDto = new List<DifficultyDto>();

            if (Difficulties.Count == 0)
            {
                return NotFound("No difficulties found.");
            }

            foreach (var difficulty in Difficulties)
            {
                DifficultiesDto.Add(new DifficultyDto()
                {
                    Id = difficulty.Id,
                    Name = difficulty.Name
                });
            }


            return Ok(DifficultiesDto);

        }

        // GET: api/Difficulty/5
        [HttpGet("{id}")]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var difficulty = dbContext.Difficulties.Find(id);

            if (difficulty is null)
            {
                return NotFound("Difficulty not found.");
            }

            var difficultyDto = new DifficultyDto()
            {
                Id = difficulty.Id,
                Name = difficulty.Name
            };

            return Ok(difficultyDto);

        }

        // POST: api/Difficulty
        [HttpPost]

        public IActionResult Create([FromBody] AddDifficultyRequestDto addDifficultyRequestDto)
        {
            var difficulty = new Difficulty
            {
                Name = addDifficultyRequestDto.Name
            };

            dbContext.Difficulties.Add(difficulty);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = difficulty.Id }, difficulty);
        }

        // PUT: api/Difficulty/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateDifficultyRequest updateDifficultyRequestDto)
        {
            var difficulty = dbContext.Difficulties.Find(id);

            if (difficulty is null)
            {
                return NotFound("Difficulty not found.");
            }

            difficulty.Name = updateDifficultyRequestDto.Name;

            dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Difficulty/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var difficulty = dbContext.Difficulties.Find(id);

            if (difficulty is null)
            {
                return NotFound("Difficulty not found.");
            }

            dbContext.Difficulties.Remove(difficulty);
            dbContext.SaveChanges();

            return NoContent();
        }


    }
}
