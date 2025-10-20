using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
// using ShopTARgv24.Data; // <- This using statement is not needed here
using ShopTARgv24.Models.Weather;
using System.Threading.Tasks; // <-- ADDED this for async Task

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

    // This action receives the POST from the search form
    [HttpPost]
    public IActionResult SearchCity(AccuWeatherSearchViewModel model) // <-- CHANGED to accept the view model
    {
        if (ModelState.IsValid)
        {
            // Redirects to the "City" action with the city name from the form
            return RedirectToAction("City", new { city = model.CityName });
        }
        
        // If model state is invalid, just return to the Index view
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        return View();
    }
    
    // This action now correctly waits for the weather data
    public async Task<IActionResult> City(string city) // <-- CHANGED to async Task<IActionResult>
    {
        AccuLocationWeatherResultDto dto = new AccuLocationWeatherResultDto();
        
        dto.CityName = city;

        // ADDED 'await' to wait for the service call to complete
        await _weatherForecastServices.AccuWeatherResult(dto);

        AccuWeatherViewModel vm = new();
        
        vm.TempMetricValueUnit = dto.TempMetricValueUnit;
        vm.Text = dto.Text;
        vm.EndDate = dto.EndDate;
            
        return View(vm);
    }
}