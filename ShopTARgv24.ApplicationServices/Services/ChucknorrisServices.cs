using System.Net;
using Nancy.Json;
using Newtonsoft.Json;
using ShopTARgv24.Core.Dto.ChuckNorris;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.ApplicationServices.Services;

public class ChucknorrisServices : IChucknorrisServices
{
    public async Task<ChuckNorrisResultDto> ChuchNorrisResult(ChuckNorrisResultDto dto)
    {
        var url = "https://api.chucknorris.io/jokes/random";

        using (HttpClient client = new HttpClient())
        {
            string json = await client.GetStringAsync(url);
            ChuckNorrisRootDto chuckNorrisResult = JsonConvert.DeserializeObject<ChuckNorrisRootDto>(json);
            dto.CreatedAt = chuckNorrisResult.CreatedAt;
            dto.IconUrl = chuckNorrisResult.IconUrl;
            dto.Id = chuckNorrisResult.Id;
            dto.UpdatedAt = chuckNorrisResult.UpdatedAt;
            dto.Url = chuckNorrisResult.Url;
            dto.Value = chuckNorrisResult.Value;
        }
        
        return dto;
    }
}