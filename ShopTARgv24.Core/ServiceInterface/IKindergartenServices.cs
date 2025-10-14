using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;

namespace ShopTARgv24.Core.ServiceInterface;

public interface IKindergartenServices
{
    Task<Kindergarten> Create(KindergartenDto dto);
    Task<Kindergarten> DetailAsync(Guid id);
    Task<Kindergarten> Delete(Guid id);
    Task<Kindergarten> Update(KindergartenDto dto);
}