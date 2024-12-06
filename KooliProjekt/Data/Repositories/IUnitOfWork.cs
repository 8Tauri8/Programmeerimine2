using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Data.Repositories
{
    public class IUnitOfWork : Controller
    {
        private class UnitOfWork : IUnitOfWork
        {
            private readonly ApplicationDbContext _context;

            public UnitOfWork(ApplicationDbContext context)
            {
                _context = context;
            }
        }
    }
}
