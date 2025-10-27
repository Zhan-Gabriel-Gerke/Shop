using ShopTARgv24.Core.Dto.ChuckNorris;

namespace ShopTARgv24.Core.ServiceInterface;

public interface IChucknorrisServices
{
    Task<ChuckNorrisResultDto> ChuchNorrisResult(ChuckNorrisResultDto dto);
}