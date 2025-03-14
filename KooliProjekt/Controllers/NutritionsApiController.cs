using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    [Route("api/Nutrition")]
    [ApiController]
    public class NutritionApiController : ControllerBase
    {
        private readonly INutritionService _nutritionService;

        public NutritionApiController(INutritionService nutritionService)
        {
            _nutritionService = nutritionService;
        }

        // GET: api/Nutrition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nutrition>>> Get()
        {
            var result = await _nutritionService.List(1, 10000);
            return Ok(result.Results);
        }

        // GET: api/Nutrition/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nutrition>> Get(int id)
        {
            var nutrition = await _nutritionService.Get(id);

            if (nutrition == null)
            {
                return NotFound();
            }

            return nutrition;
        }

        // POST: api/Nutrition
        [HttpPost]
        public async Task<ActionResult<Nutrition>> Post([FromBody] Nutrition nutrition)
        {
            await _nutritionService.Save(nutrition);

            return CreatedAtAction(nameof(Get), new { id = nutrition.id }, nutrition);
        }

        // PUT: api/Nutrition/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Nutrition nutrition)
        {
            if (id != nutrition.id)
            {
                return BadRequest();
            }

            var existingNutrition = await _nutritionService.Get(id);
            if (existingNutrition == null)
            {
                return NotFound();
            }

            await _nutritionService.Save(nutrition);

            return NoContent();
        }

        // DELETE: api/Nutrition/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nutrition = await _nutritionService.Get(id);
            if (nutrition == null)
            {
                return NotFound();
            }

            await _nutritionService.Delete(id);

            return NoContent();
        }
    }
}
