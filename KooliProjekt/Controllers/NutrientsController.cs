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

        public IActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Sugar,Fat,Carbohydrates")] Nutrients nutrients)
        {
            if (ModelState.IsValid)
            {
                await _NutrientsService.Save(nutrients); 
                return RedirectToAction(nameof(Index));
            }
            return View(nutrients);
        }

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

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Sugar,Fat,Carbohydrates")] Nutrients nutrients)
        {
            if (id != nutrients.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _NutrientsService.Save(nutrients); 
                return RedirectToAction(nameof(Index));
            }

            return View(nutrients);
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NutrientsService.Delete(id); 
            return RedirectToAction(nameof(Index));
        }
    }
}