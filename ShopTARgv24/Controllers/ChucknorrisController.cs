using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.Dto.ChuckNorris;
using ShopTARgv24.Models.Chucknorris;
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

    [HttpPost]
    public IActionResult SearchChucknorrisJokes()
    {
        return RedirectToAction(nameof(Joke));
    }

    [HttpGet]
    public IActionResult Joke()
    {
        ChuckNorrisResultDto dto = new();

        _chucknorrisServices.ChuchNorrisResult(dto);
        ChucknorrisViewModel vm = new();
        
        vm.Categories = dto.Categories;
        vm.CreatedAt = dto.CreatedAt;
        vm.IconUrl = dto.IconUrl;
        vm.Id = dto.Id;
        vm.UpdatedAt = dto.UpdatedAt;
        vm.Url = dto.Url;
        vm.Value = dto.Value;
        
        return View(vm);
    }
}