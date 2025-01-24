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

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _QuantityService.List(page, pageSize));
        }

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nutrients,Amount")] Quantity quantity)
        {
            if (ModelState.IsValid)
            {
                await _QuantityService.Save(quantity); 
                return RedirectToAction(nameof(Index));
            }
            return View(quantity);
        }

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
                await _QuantityService.Save(quantity);  
                return RedirectToAction(nameof(Index));
            }
            return View(quantity);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _QuantityService.Delete(id);  
            return RedirectToAction(nameof(Index));
        }
    }
}