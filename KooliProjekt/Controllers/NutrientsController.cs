using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly INutrientsService _nutrientsService;

        public NutrientsController(INutrientsService nutrientsService)
        {
            _nutrientsService = nutrientsService;
        }

        public async Task<IActionResult> Index(int page, NutrientsIndexModel model)
        {
            // Ensure model and its properties are initialized
            if (model == null)
            {
                model = new NutrientsIndexModel();
            }
            if (model.Search == null)
            {
                model.Search = new NutrientsSearch();
            }

            // Fetch paginated data using the search criteria
            var pagedResult = await _nutrientsService.List(page, 5, model.Search);
            model.Data = pagedResult;

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var nutrient = await _nutrientsService.Get(id.Value);
            if (nutrient == null)
                return NotFound();

            return View(nutrient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Nutrients nutrient)
        {
            if (ModelState.IsValid)
            {
                await _nutrientsService.Save(nutrient);
                return RedirectToAction(nameof(Index));
            }

            return View(nutrient);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var nutrient = await _nutrientsService.Get(id.Value);
            if (nutrient == null)
                return NotFound();

            return View(nutrient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Nutrients nutrient)
        {
            if (id != nutrient.id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _nutrientsService.Save(nutrient);
                return RedirectToAction(nameof(Index));
            }

            return View(nutrient);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var nutrient = await _nutrientsService.Get(id.Value);
            if (nutrient == null)
                return NotFound();

            return View(nutrient);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nutrientsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
