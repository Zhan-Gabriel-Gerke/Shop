using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Models.Weather;

namespace ShopTARgv24.Controllers;

public class ChucknorrisController : Controller
{
    private readonly IChucknorrisServices _chucknorrisServices;

    public ChucknorrisController
    (
        IChucknorrisServices chucknorrisServices
    )
    {
        _chucknorrisServices = chucknorrisServices;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }
}