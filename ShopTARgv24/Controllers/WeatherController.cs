using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.Controllers;

public class WeatherController : Controller
{
    private readonly IWeatherForecastServices _weatherForecastServices;

    public WeatherController
    (
        IWeatherForecastServices weatherForecastServices
    )
    {
        _weatherForecastServices = weatherForecastServices;
    }

    //teha action SearchCity
    [HttpPost]
    public IActionResult SearchCity()
    {
        return View();
    }
}