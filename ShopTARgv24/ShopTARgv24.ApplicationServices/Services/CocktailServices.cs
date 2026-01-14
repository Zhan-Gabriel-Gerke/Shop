using ShopTARgv24.Core.Dto.CocktailDto;
using ShopTARgv24.Core.ServiceInterface;
using System.Text.Json;
namespace ShopTARgv24.ApplicationServices.Services;

public class CocktailServices : ICocktailServices
{
    private readonly HttpClient _httpClient;

    public CocktailServices
    (
        HttpClient httpClient
    )
    {
        _httpClient = httpClient;
    }

    public async Task<CocktailRootDto> GetCocktails(CocktailResultDto dto)
    {
        string apiKey = "1";
        var response = await _httpClient.GetAsync($"https://www.thecocktaildb.com/api/json/v1/{apiKey}/search.php?s={dto.StrDrink}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var cocktail = JsonSerializer.Deserialize<CocktailRootDto>(json, options);

        return cocktail!;
    }
}