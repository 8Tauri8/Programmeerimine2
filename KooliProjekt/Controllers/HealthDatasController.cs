// File: Controllers/FoodChartsController.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class HealthDatasController : Controller
    {
        private readonly IHealthDataService _HealthDataService;

        public HealthDatasController(IHealthDataService HealthDataService)
        {
            _HealthDataService = HealthDataService;
        }

        // GET: HealthDatas
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _HealthDataService.List(page, pageSize));
        }

        // GET: FoodCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await _HealthDataService.Get(id.Value);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        // GET: FoodCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCharts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Weight,Blood_pressure,Blood_sugar")] HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                await _HealthDataService.Save(healthData);  // Save new food chart
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        // GET: FoodCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await _HealthDataService.Get(id.Value);
            if (healthData == null)
            {
                return NotFound();
            }
            return View(healthData);
        }

        // POST: FoodCharts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Weight,Blood_pressure,Blood_sugar")] HealthData healthData)
        {
            if (id != healthData.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _HealthDataService.Save(healthData);  // Save updated food chart
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        // GET: FoodCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await _HealthDataService.Get(id.Value);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        // POST: FoodCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _HealthDataService.Delete(id);  // Delete food chart
            return RedirectToAction(nameof(Index));
        }
    }
}