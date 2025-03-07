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

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            return View(await _NutritionService.List(page, pageSize));
        }

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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Eating_time,Nutrients,Quantity")] Nutrition nutrition)
        {
            if (ModelState.IsValid)
            {
                await _NutritionService.Save(nutrition);
                return RedirectToAction(nameof(Index));
            }
            return View(nutrition);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Eating_time,Nutrients,Quantity")] Nutrition nutrition)
        {
            if (id != nutrition.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _NutritionService.Save(nutrition);  
                return RedirectToAction(nameof(Index));
            }
            return View(nutrition);
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _NutritionService.Delete(id);  
            return RedirectToAction(nameof(Index));
        }
    }
}