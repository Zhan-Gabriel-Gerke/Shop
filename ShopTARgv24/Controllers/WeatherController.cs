using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Models.Weather;

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

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult City(string city)
    {
        AccuLocationWeatherResultDto dto = new AccuLocationWeatherResultDto();
        
        dto.CityName = city;

        _weatherForecastServices.AccuWeatherResult(dto);

        AccuWeatherViewModel vm = new();
        
        vm.TempMetricValueUnit = dto.TempMetricValueUnit;
        vm.Text = dto.Text;
        vm.EndDate = dto.EndDate;
            
        return View(vm);
    }
}