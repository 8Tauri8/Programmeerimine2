using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    [Route("api/Nutrients")]
    [ApiController]
    public class NutrientsApiController : ControllerBase
    {
        private readonly INutrientsService _nutrientsService;

        public NutrientsApiController(INutrientsService nutrientsService)
        {
            _nutrientsService = nutrientsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nutrients>>> Get()
        {
            var result = await _nutrientsService.List(1, 10000, null); // Added null here
            return Ok(result.Results);
        }

        // Other actions remain unchanged
        [HttpGet("{id}")]
        public async Task<ActionResult<Nutrients>> Get(int id)
        {
            var nutrient = await _nutrientsService.Get(id);
            if (nutrient == null) return NotFound();
            return nutrient;
        }

        [HttpPost]
        public async Task<ActionResult<Nutrients>> Post([FromBody] Nutrients nutrient)
        {
            await _nutrientsService.Save(nutrient);
            return CreatedAtAction(nameof(Get), new { id = nutrient.id }, nutrient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Nutrients nutrient)
        {
            if (id != nutrient.id) return BadRequest("ID mismatch");

            var existing = await _nutrientsService.Get(id);
            if (existing == null) return NotFound();

            // Update properties here for proper patching
            existing.Name = nutrient.Name;
            existing.Sugar = nutrient.Sugar;
            existing.Fat = nutrient.Fat;
            existing.Carbohydrates = nutrient.Carbohydrates;

            await _nutrientsService.Save(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nutrient = await _nutrientsService.Get(id);
            if (nutrient == null) return NotFound();

            await _nutrientsService.Delete(id);
            return NoContent();
        }
    }
}
