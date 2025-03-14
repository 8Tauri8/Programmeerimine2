using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientsApiController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsApiController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {
            var result = await _patientService.List(1, 10000, null);
            return Ok(result.Results);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _patientService.Get(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> Post([FromBody] Patient patient)
        {
            await _patientService.Save(patient);

            return CreatedAtAction(nameof(Get), new { id = patient.id }, patient);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Patient patient)
        {
            if (id != patient.id)
            {
                return BadRequest();
            }

            await _patientService.Save(patient);

            return Ok();
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientService.Get(id);
            if (patient == null)
            {
                return NotFound();
            }

            await _patientService.Delete(id);

            return NoContent();
        }
    }
}
