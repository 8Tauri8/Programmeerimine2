﻿using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Search;

namespace KooliProjekt.Controllers
{
    public class HealthDatasController : Controller
    {
        private readonly IHealthDataService _healthDataService;

        public HealthDatasController(IHealthDataService healthDataService)
        {
            _healthDataService = healthDataService;
        }

        public async Task<IActionResult> Index(int page = 1, HealthDatasIndexModel model = null)
        {
            model = model ?? new HealthDatasIndexModel();
            int pageSize = 5;

            model.Data = await _healthDataService.List(page, pageSize, model.Search);
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await _healthDataService.Get(id.Value);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Weight,Blood_pressure,Blood_sugar")] HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                await _healthDataService.Save(healthData);
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await _healthDataService.Get(id.Value);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

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
                await _healthDataService.Save(healthData);
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await _healthDataService.Get(id.Value);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _healthDataService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
