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
            return View(await HealthDataService.HealthData.GetPagedAsync(page, 5));
        }

        // GET: HealthDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await HealthDataService.HealthData
                .FirstOrDefaultAsync(m => m.id == id);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        // GET: HealthDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HealthDataID,Weight,Blood_pressure,Blood_sugar")] HealthData healthData)
        {
            if (ModelState.IsValid)
            {
                HealthDataService.Add(healthData);
                await HealthDataService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        // GET: HealthDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await HealthDataService.HealthData.FindAsync(id);
            if (healthData == null)
            {
                return NotFound();
            }
            return View(healthData);
        }

        // POST: HealthDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HealthDataID,Weight,Blood_pressure,Blood_sugar")] HealthData healthData)
        {
            if (id != healthData.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HealthDataService.Update(healthData);
                    await HealthDataService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthDataExists(healthData.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(healthData);
        }

        // GET: HealthDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthData = await HealthDataService.HealthData
                .FirstOrDefaultAsync(m => m.id == id);
            if (healthData == null)
            {
                return NotFound();
            }

            return View(healthData);
        }

        // POST: HealthDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthData = await HealthDataService.HealthData.FindAsync(id);
            if (healthData != null)
            {
                HealthDataService.HealthData.Remove(healthData);
            }

            await HealthDataService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthDataExists(int id)
        {
            return HealthDataService.HealthData.Any(e => e.id == id);
        }
    }
}
