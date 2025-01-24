// File: Controllers/FoodChartsController.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class QuantitiesController : Controller
    {
        private readonly IQuantityService _QuantityService;

        public QuantitiesController(IQuantityService QuantityService)
        {
            _QuantityService = QuantityService;
        }

        // GET: Quantitys
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _QuantityService.List(page, pageSize));
        }

        // GET: FoodCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantity = await _QuantityService.Get(id.Value);
            if (quantity == null)
            {
                return NotFound();
            }

            return View(quantity);
        }

        // GET: FoodCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCharts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nutrients,Amount")] Quantity quantity)
        {
            if (ModelState.IsValid)
            {
                await _QuantityService.Save(quantity);  // Save new food chart
                return RedirectToAction(nameof(Index));
            }
            return View(quantity);
        }

        // GET: FoodCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantity = await _QuantityService.Get(id.Value);
            if (quantity == null)
            {
                return NotFound();
            }
            return View(quantity);
        }

        // POST: FoodCharts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nutrients,Amount")] Quantity quantity)
        {
            if (id != quantity.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _QuantityService.Save(quantity);  // Save updated food chart
                return RedirectToAction(nameof(Index));
            }
            return View(quantity);
        }

        // GET: FoodCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantity = await _QuantityService.Get(id.Value);
            if (quantity == null)
            {
                return NotFound();
            }

            return View(quantity);
        }

        // POST: FoodCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _QuantityService.Delete(id);  // Delete food chart
            return RedirectToAction(nameof(Index));
        }
    }
}