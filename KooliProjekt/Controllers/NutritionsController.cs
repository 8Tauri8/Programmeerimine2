// File: Controllers/FoodChartsController.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class NutritionsController : Controller
    {
        private readonly INutritionService _NutritionService;

        public NutritionsController(INutritionService NutritionService)
        {
            _NutritionService = NutritionService;
        }

        // GET: HealthDatas
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _NutritionService.List(page, pageSize));
        }

        // GET: FoodCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrition = await _NutritionService.Get(id.Value);
            if (nutrition == null)
            {
                return NotFound();
            }

            return View(nutrition);
        }

        // GET: FoodCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCharts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceNo,InvoiceDate,user,date,meal,nutrients,amount")] Nutrition nutrition)
        {
            if (ModelState.IsValid)
            {
                await _NutritionService.Save(nutrition);  // Save new food chart
                return RedirectToAction(nameof(Index));
            }
            return View(nutrition);
        }

        // GET: FoodCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrition = await _NutritionService.Get(id.Value);
            if (nutrition == null)
            {
                return NotFound();
            }
            return View(nutrition);
        }

        // POST: FoodCharts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNo,InvoiceDate,user,date,meal,nutrients,amount")] Nutrition nutrition)
        {
            if (id != nutrition.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _NutritionService.Save(nutrition);  // Save updated food chart
                return RedirectToAction(nameof(Index));
            }
            return View(nutrition);
        }

        // GET: FoodCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodChart = await _NutritionService.Get(id.Value);
            if (foodChart == null)
            {
                return NotFound();
            }

            return View(foodChart);
        }

        // POST: FoodCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NutritionService.Delete(id);  // Delete food chart
            return RedirectToAction(nameof(Index));
        }
    }
}