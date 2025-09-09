using Microsoft.AspNetCore.Mvc;
using Shop.Models.SpaceShips;
using Shop.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shop.Core.ServiceInterface;
using Shop.Core.Dto;

namespace Shop.Controllers
{
    public class SpaceShipsController : Controller
    {

        private readonly ShopContext _context;
        private readonly ISpaceShipsServices _spaceShipsServices;

        public SpaceShipsController
            (
                ShopContext context,
                ISpaceShipsServices spaceShipsServices
            )
        {
            _context = context;
            _spaceShipsServices = spaceShipsServices;
        }
        public IActionResult Index()
        {
            var result = _context.SpaceShips
                .Select(x => new SpaceShipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BuiltDate = x.BuiltDate,
                    TypeName = x.TypeName,
                    Crew = x.Crew

                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            SpaceShipCreateModel result = new();

            return View("Create", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpaceShipCreateModel vm)
        {
            var dto = new SpaceShipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                TypeName = vm.TypeName,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                Passengers = vm.Passengers,
                InnerVolume = vm.InnerVolume,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _spaceShipsServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(result);
        }
    }
}
