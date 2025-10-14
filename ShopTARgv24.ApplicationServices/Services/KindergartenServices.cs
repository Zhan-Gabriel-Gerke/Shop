using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices.Services;

public class KindergartenServices : IKindergartenServices
{
    private readonly ShopTARgv24Context context;
    private readonly IFileServices _fileServices;

    public KindergartenServices(
        ShopTARgv24Context context,
        IFileServices fileServices
    )
    {
        this.context = context;
        _fileServices = fileServices;
    }

    public async Task<Kindergarten> DetailAsync(Guid? id)
    {
        var result = await context.Kindergartens
            .FirstOrDefaultAsync(x => x.KindergartenId == id);
        return result;
    }
    public async Task<Kindergarten> Create(KindergartenDto dto)
    {
        Kindergarten kindergarten = new Kindergarten();
        
        kindergarten.KindergartenId = Guid.NewGuid();
        kindergarten.GroupName = dto.GroupName;
        kindergarten.ChildrenCount = dto.ChildrenCount;
        kindergarten.KindergartenName = dto.KindergartenName;
        kindergarten.TeacherName = dto.TeacherName;
        kindergarten.CreatedAt = DateTime.Now;
        kindergarten.ModifiedAt = DateTime.Now;

        if (dto.Files != null)
        {
            _fileServices.UploadFilesToDatabase(dto, kindergarten);
        }
        
        await context.Kindergartens.AddAsync(kindergarten);
        await context.SaveChangesAsync();
        
        return kindergarten;
    }

    public async Task<Kindergarten> Delete(Guid? id)
    {
        var kindergarten = await context.Kindergartens
            .FirstOrDefaultAsync(x => x.KindergartenId == id);
        if (kindergarten == null)
        {
            return null;
        }
        
        context.Kindergartens.Remove(kindergarten);
        await context.SaveChangesAsync();
        var images = await context.FileToDatabases
            .Where(x => x.KindergartenId == id)
            .Select(y => new FileToDatabaseDto
            {
                Id = y.Id,
                ImageData = y.ImageData,
                ImageTitle = y.ImageTitle,
                KindergartenId = y.KindergartenId
            }).ToArrayAsync();

        await _fileServices.RemoveImageFromDatabase(images);
        context.Kindergartens.Remove(kindergarten);
        await context.SaveChangesAsync();
        return kindergarten;
    }

    public async Task<Kindergarten> Update(KindergartenDto dto)
    {
        Kindergarten domain = new();

        domain.KindergartenId = dto.KindergartenId;
        domain.GroupName = dto.GroupName;
        domain.ChildrenCount = dto.ChildrenCount;
        domain.KindergartenName = dto.KindergartenName;
        domain.TeacherName = dto.TeacherName;
        domain.CreatedAt = dto.CreatedAt;
        domain.ModifiedAt = DateTime.Now;
        
        _fileServices.UploadFilesToDatabase(dto, domain);
        
        context.Kindergartens.Update(domain);
        await context.SaveChangesAsync();
        
        return domain;
    }
    
}