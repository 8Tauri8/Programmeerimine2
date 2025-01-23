using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly INutrientsService _NutrientsService;

        public NutrientsController(INutrientsService NutrientsService)
        {
            _NutrientsService = NutrientsService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _NutrientsService.List(page, pageSize));
        }

        // GET: Nutrients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Nutrients = await _NutrientsService.Get(id.Value);
            if (Nutrients == null)
            {
                return NotFound();
            }

            return View(Nutrients);
        }

        // GET: Nutrients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nutrients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NutrientsID,Name,Sugar,Fat,Carbohydrates")] Nutrients nutrients)
        {
            if (ModelState.IsValid)
            {
                await _NutrientsService.Save(nutrients);  // Save new food chart
                return RedirectToAction(nameof(Index));
            }
            return View(nutrients);
        }

        // GET: Nutrients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrients = await _NutrientsService.Get(id.Value);
            if (nutrients == null)
            {
                return NotFound();
            }
            return View(nutrients);
        }

        // POST: Nutrients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NutrientsID,Name,Sugar,Fat,Carbohydrates")] Nutrients nutrients)
        {
            if (id != nutrients.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _NutrientsService.Save(nutrients);  // Save updated food chart
                return RedirectToAction(nameof(Index));
            }

            return View(nutrients);
        }

        // GET: Nutrients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodChart = await _NutrientsService.Get(id.Value);
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
            await _NutrientsService.Delete(id);  // Delete food chart
            return RedirectToAction(nameof(Index));
        }
    }
}