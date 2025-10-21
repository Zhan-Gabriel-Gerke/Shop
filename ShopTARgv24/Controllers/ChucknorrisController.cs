using Microsoft.AspNetCore.Mvc;

namespace ShopTARgv24.Controllers;

public class ChucknorrisController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}