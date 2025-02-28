using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly INutrientsService _NutrientsService;

        public NutrientsController(INutrientsService NutrientsService)
        {
            _NutrientsService = NutrientsService;
        }

        public async Task<IActionResult> Index(int page = 1, NutrientsIndexModel model = null)
        {
            // Ensure the model is not null
            model = model ?? new NutrientsIndexModel();

            // Initialize the Search property if it's null
            model.Search = model.Search ?? new NutrientsSearch();

            int pageSize = 5;

            // Map form data to NutrientsSearch
            if (HttpContext.Request.Query.ContainsKey("Name"))
            {
                model.Search.Name = HttpContext.Request.Query["Name"].ToString();
            }
            if (HttpContext.Request.Query.ContainsKey("MinSugar"))
            {
                if (float.TryParse(HttpContext.Request.Query["MinSugar"], out float minSugar))
                {
                    model.Search.MinSugar = minSugar;
                }
            }
            if (HttpContext.Request.Query.ContainsKey("MaxSugar"))
            {
                if (float.TryParse(HttpContext.Request.Query["MaxSugar"], out float maxSugar))
                {
                    model.Search.MaxSugar = maxSugar;
                }
            }
            if (HttpContext.Request.Query.ContainsKey("MinFat"))
            {
                if (float.TryParse(HttpContext.Request.Query["MinFat"], out float minFat))
                {
                    model.Search.MinFat = minFat;
                }
            }
            if (HttpContext.Request.Query.ContainsKey("MaxFat"))
            {
                if (float.TryParse(HttpContext.Request.Query["MaxFat"], out float maxFat))
                {
                    model.Search.MaxFat = maxFat;
                }
            }
            if (HttpContext.Request.Query.ContainsKey("MinCarbohydrates"))
            {
                if (float.TryParse(HttpContext.Request.Query["MinCarbohydrates"], out float minCarbohydrates))
                {
                    model.Search.MinCarbohydrates = minCarbohydrates;
                }
            }
            if (HttpContext.Request.Query.ContainsKey("MaxCarbohydrates"))
            {
                if (float.TryParse(HttpContext.Request.Query["MaxCarbohydrates"], out float maxCarbohydrates))
                {
                    model.Search.MaxCarbohydrates = maxCarbohydrates;
                }
            }

            try
            {
                // Fetch data from the service
                model.Data = await _NutrientsService.List(page, pageSize, model.Search);
            }
            catch (Exception ex)
            {
                // Log the exception and initialize an empty PagedResult
                Console.WriteLine($"Error fetching data: {ex.Message}");
                model.Data = new PagedResult<Nutrients>
                {
                    Results = new List<Nutrients>(),
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = 0
                };
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
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
