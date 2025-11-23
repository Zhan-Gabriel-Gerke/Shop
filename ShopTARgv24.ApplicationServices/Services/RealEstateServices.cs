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
        
        await context.RealEstates.AddAsync(realEstate);
        await context.SaveChangesAsync();
        
        return realEstate;
    }

    // public async Task<RealEstate> Delete(Guid? id)
    // {
    //     var realEstate = await context.RealEstates
    //         .FirstOrDefaultAsync(x => x.Id == id);
    //     if (realEstate == null)
    //     {
    //         return null;
    //     }
    //     
    //     context.RealEstates.Remove(realEstate);
    //     await context.SaveChangesAsync();
    //     
    //     return realEstate;
    // }

    public async Task<RealEstate> Delete(Guid id)
    {
        var result = await context.RealEstates
            .FirstOrDefaultAsync(x => x.Id == id);

        var images = await context.FileToDatabases
            .Where(x => x.RealEstateId == result.Id)
            .Select(x => new FileToDatabaseDto
            {
                Id = x.Id,
                ImageTitle = x.ImageTitle,
                RealEstateId = x.RealEstateId
            }).ToArrayAsync();

        await _fileServices.RemoveImageFromDatabase(images);
        context.RealEstates.Remove(result);
        await context.SaveChangesAsync();
        return result;
    }

    public async Task<RealEstate> Update(RealEstateDto dto)
    {
        var existingRealEstate = await context.RealEstates.FindAsync(dto.Id);
        if (existingRealEstate == null)
        {
            return null;
        }
        existingRealEstate.Area = dto.Area;
        existingRealEstate.Location = dto.Location;
        existingRealEstate.RoomNumber = dto.RoomNumber;
        existingRealEstate.BuildingType = dto.BuildingType;
        existingRealEstate.ModifiedAt = DateTime.Now;
        if (dto.Files != null)
        {
            _fileServices.UploadFilesToDatabase(dto, existingRealEstate);
        }
        await context.SaveChangesAsync();
        return existingRealEstate;
    }
    
    
}