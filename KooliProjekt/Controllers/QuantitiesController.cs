﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class QuantitiesController : Controller
    {
        private readonly ApplicationDbContext QuantityService;

        public QuantitiesController(ApplicationDbContext context)
        {
            QuantityService = context;
        }

        // GET: Quantities
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await QuantityService.Quantity.GetPagedAsync(page, 5));
        }

        // GET: Quantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantity = await QuantityService.Quantity
                .FirstOrDefaultAsync(m => m.id == id);
            if (quantity == null)
            {
                return NotFound();
            }

            return View(quantity);
        }

        // GET: Quantities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quantities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuantityID,Amount")] Quantity quantity)
        {
            if (ModelState.IsValid)
            {
                QuantityService.Add(quantity);
                await QuantityService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quantity);
        }

        // GET: Quantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantity = await QuantityService.Quantity.FindAsync(id);
            if (quantity == null)
            {
                return NotFound();
            }
            return View(quantity);
        }

        // POST: Quantities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuantityID,Amount")] Quantity quantity)
        {
            if (id != quantity.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    QuantityService.Update(quantity);
                    await QuantityService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuantityExists(quantity.id))
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
            return View(quantity);
        }

        // GET: Quantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quantity = await QuantityService.Quantity
                .FirstOrDefaultAsync(m => m.id == id);
            if (quantity == null)
            {
                return NotFound();
            }

            return View(quantity);
        }

        // POST: Quantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quantity = await QuantityService.Quantity.FindAsync(id);
            if (quantity != null)
            {
                QuantityService.Quantity.Remove(quantity);
            }

            await QuantityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuantityExists(int id)
        {
            return QuantityService.Quantity.Any(e => e.id == id);
        }
    }
}
