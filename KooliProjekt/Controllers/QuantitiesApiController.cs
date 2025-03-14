using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    [Route("api/Quantities")]
    [ApiController]
    public class QuantitiesApiController : ControllerBase
    {
        private readonly IQuantityService _quantityService;

        public QuantitiesApiController(IQuantityService quantityService)
        {
            _quantityService = quantityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quantity>>> Get()
        {
            var result = await _quantityService.List(1, 10000);
            return Ok(result.Results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quantity>> Get(int id)
        {
            var quantity = await _quantityService.Get(id);
            if (quantity == null)
            {
                return NotFound();
            }
            return quantity;
        }

        [HttpPost]
        public async Task<ActionResult<Quantity>> Post([FromBody] Quantity quantity)
        {
            await _quantityService.Save(quantity);
            return CreatedAtAction(nameof(Get), new { id = quantity.id }, quantity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Quantity quantity)
        {
            if (id != quantity.id)
            {
                return BadRequest();
            }

            var existingQuantity = await _quantityService.Get(id);
            if (existingQuantity == null)
            {
                return NotFound();
            }

            await _quantityService.Save(quantity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var quantity = await _quantityService.Get(id);
            if (quantity == null)
            {
                return NotFound();
            }

            await _quantityService.Delete(id);
            return NoContent();
        }
    }
}
