// File: Controllers/FoodChartsController.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _PatientService;

        public PatientsController(IPatientService patientService)
        {
            _PatientService = patientService;
        }

        // GET: patients
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _PatientService.List(page, pageSize));
        }

        // GET: FoodCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _PatientService.Get(id.Value);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: FoodCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCharts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,HealthData,Nutrition")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _PatientService.Save(patient);  // Save new food chart
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: FoodCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _PatientService.Get(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: FoodCharts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,HealthData,Nutrition")] Patient patient)
        {
            if (id != patient.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _PatientService.Save(patient);  // Save updated food chart
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: FoodCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _PatientService.Get(id.Value);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: FoodCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _PatientService.Delete(id);  // Delete food chart
            return RedirectToAction(nameof(Index));
        }
    }
}