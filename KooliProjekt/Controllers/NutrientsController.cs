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
    public class NutrientsController : Controller
    {
        private readonly ApplicationDbContext NutrientsService;

        public NutrientsController(ApplicationDbContext context)
        {
            NutrientsService = context;
        }

        // GET: Nutrients
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await NutrientsService.Nutrients.GetPagedAsync(page, 5));
        }

        // GET: Nutrients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrients = await NutrientsService.Nutrients
                .FirstOrDefaultAsync(m => m.id == id);
            if (nutrients == null)
            {
                return NotFound();
            }

            return View(nutrients);
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
                NutrientsService.Add(nutrients);
                await NutrientsService.SaveChangesAsync();
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

            var nutrients = await NutrientsService.Nutrients.FindAsync(id);
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
                try
                {
                    NutrientsService.Update(nutrients);
                    await NutrientsService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutrientsExists(nutrients.id))
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
            return View(nutrients);
        }

        // GET: Nutrients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrients = await NutrientsService.Nutrients
                .FirstOrDefaultAsync(m => m.id == id);
            if (nutrients == null)
            {
                return NotFound();
            }

            return View(nutrients);
        }

        // POST: Nutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nutrients = await NutrientsService.Nutrients.FindAsync(id);
            if (nutrients != null)
            {
                NutrientsService.Nutrients.Remove(nutrients);
            }

            await NutrientsService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutrientsExists(int id)
        {
            return NutrientsService.Nutrients.Any(e => e.id == id);
        }
    }
}
