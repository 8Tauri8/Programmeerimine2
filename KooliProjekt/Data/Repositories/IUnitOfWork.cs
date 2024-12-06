using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Data.Repositories
{
    public class IUnitOfWork : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
