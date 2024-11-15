using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class NutritionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NutritionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nutritions
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _context.Nutrition.GetPagedAsync(page, 5));
        }

        // GET: Nutritions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrition = await _context.Nutrition
                .FirstOrDefaultAsync(m => m.id == id);
            if (nutrition == null)
            {
                return NotFound();
            }

            return View(nutrition);
        }

        // GET: Nutritions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nutritions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NutritionID,Eating_time")] Nutrition nutrition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nutrition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nutrition);
        }

        // GET: Nutritions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrition = await _context.Nutrition.FindAsync(id);
            if (nutrition == null)
            {
                return NotFound();
            }
            return View(nutrition);
        }

        // POST: Nutritions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NutritionID,Eating_time")] Nutrition nutrition)
        {
            if (id != nutrition.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nutrition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutritionExists(nutrition.id))
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
            return View(nutrition);
        }

        // GET: Nutritions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrition = await _context.Nutrition
                .FirstOrDefaultAsync(m => m.id == id);
            if (nutrition == null)
            {
                return NotFound();
            }

            return View(nutrition);
        }

        // POST: Nutritions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nutrition = await _context.Nutrition.FindAsync(id);
            if (nutrition != null)
            {
                _context.Nutrition.Remove(nutrition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutritionExists(int id)
        {
            return _context.Nutrition.Any(e => e.id == id);
        }
    }
}
