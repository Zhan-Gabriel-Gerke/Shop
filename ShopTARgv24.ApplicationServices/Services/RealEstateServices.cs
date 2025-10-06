using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices.Services;

public class RealEstateServices : IRealEstateServices
{
    private readonly ShopTARgv24Context context;
    private readonly IFileServices _fileServices;

    public RealEstateServices(
        ShopTARgv24Context context,
        IFileServices fileServices
    )
    {
        this.context = context;
        _fileServices = fileServices;
    }

    public async Task<RealEstate> DetailAsync(Guid? id)
    {
        var result = await context.RealEstates
            .FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }
    public async Task<RealEstate> Create(RealEstateDto dto)
    {
        RealEstate realEstate = new RealEstate();
        
        realEstate.Id = Guid.NewGuid();
        realEstate.Area = dto.Area;
        realEstate.Location = dto.Location;
        realEstate.RoomNumber = dto.RoomNumber;
        realEstate.BuildingType = dto.BuildingType;
        realEstate.CreatedAt = DateTime.Now;
        realEstate.ModifiedAt = DateTime.Now;

        if (dto.Files != null)
        {
            _fileServices.UploadFilesToDatabase(dto, realEstate);
        }
        
        _fileServices.UploadFilesToDatabase(dto, realEstate);
        await context.RealEstates.AddAsync(realEstate);
        await context.SaveChangesAsync();
        
        return realEstate;
    }

    public async Task<RealEstate> Delete(Guid? id)
    {
        var realEstate = await context.RealEstates
            .FirstOrDefaultAsync(x => x.Id == id);
        if (realEstate == null)
        {
            return null;
        }
        
        context.RealEstates.Remove(realEstate);
        await context.SaveChangesAsync();
        
        return realEstate;
    }

    public async Task<RealEstate> Update(RealEstateDto dto)
    {
        RealEstate domain = new();

        domain.Id = dto.Id;
        domain.Area = dto.Area;
        domain.Location = dto.Location;
        domain.RoomNumber = dto.RoomNumber;
        domain.BuildingType = dto.BuildingType;
        domain.CreatedAt = dto.CreatedAt;
        domain.ModifiedAt = DateTime.Now;
        
        context.RealEstates.Update(domain);
        await context.SaveChangesAsync();
        
        return domain;
    }
}