using Microsoft.AspNetCore.Mvc;
using Shop.Models.SpaceShips;
using Shop.Data;

namespace Shop.Controllers
{
    public class SpaceShipsController : Controller
    {

        private readonly ShopContext _context;

        public SpaceShipsController
            (
                ShopContext context
            )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.SpaceShips
                .Select(x => new SpaceShipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BuiltDate = x.BuiltDate,
                    TypeName = x.TypeName

                });
            return View(result);
        }
    }
}
