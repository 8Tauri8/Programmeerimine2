using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    [Route("api/HealthData")]
    [ApiController]
    public class HealthDataApiController : ControllerBase
    {
        private readonly IHealthDataService _healthDataService;

        public HealthDataApiController(IHealthDataService healthDataService)
        {
            _healthDataService = healthDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthData>>> Get()
        {
            var result = await _healthDataService.List(1, 10000);
            return Ok(result.Results);
        }

        // GET: api/HealthData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthData>> Get(int id)
        {
            var healthData = await _healthDataService.Get(id);

            if (healthData == null)
            {
                return NotFound();
            }

            return healthData;
        }

        // POST: api/HealthData
        [HttpPost]
        public async Task<ActionResult<HealthData>> Post([FromBody] HealthData healthData)
        {
            await _healthDataService.Save(healthData);

            return CreatedAtAction(nameof(Get), new { id = healthData.id }, healthData);
        }

        // PUT: api/HealthData/5
        // PUT api/<TodoListsApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HealthData list)
        {
            if (id != list.id)
            {
                return BadRequest();
            }

            await _healthDataService.Save(list);

            return Ok();
        }

        // DELETE: api/HealthData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var healthData = await _healthDataService.Get(id);
            if (healthData == null)
            {
                return NotFound();
            }

            await _healthDataService.Delete(id);

            return NoContent();
        }
    }
}
