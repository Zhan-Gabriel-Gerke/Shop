﻿using System.Text.Json;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Core.Dto;
    
namespace ShopTARgv24.ApplicationServices.Services;

public class WeatherForecastServices : IWeatherForecastServices
{
    public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
    {
        string accuApiKey = "<your_api>";
        string baseUrl = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/";

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            // GetAsync($"{127964} - Tallinna LocationKey
            var response = await httpClient.GetAsync($"{127964}?apikey={accuApiKey}&details=true");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<AccuLocationRootDto>(jsonResponse);
                //

                dto.EndDate = weatherData.Headline.EndDate;
                dto.Text = weatherData.Headline.Text;
                dto.TempMetricValueUnit = weatherData.DailyForecasts[0].Temperature.Maximum.Value;

            }
            else
            {
                // Handle error response
                throw new Exception("Error fetching weather data");
            }
            return dto;
        }
    }
}