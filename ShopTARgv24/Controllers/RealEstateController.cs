using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Models.RealEstate;
using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Dto;

namespace ShopTARgv24.Controllers;

public class RealEstateController : Controller
{
    private readonly ShopTARgv24Context _context;
    private readonly IRealEstateServices _realEstateServices;
    

    public RealEstateController
    (
        ShopTARgv24Context context,
        IRealEstateServices realEstateServices
        
    )
    {
        _context = context;
        _realEstateServices = realEstateServices;
        
    }
    [HttpGet]
    public IActionResult Index()
    {
        var result = _context.RealEstates
            .Select(x => new RealEstateIndexView
            {
                Id = x.Id,
                Area = x.Area,
                Location = x.Location,
                RoomNumber = x.RoomNumber,
                BuildingType = x.BuildingType
            });
        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        RealEstateCreateUpdateViewModel result = new();
        
        return View("CreateUpdate" ,result);
    }

    [HttpPost]

    public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
    {
        var dto = new RealEstateDto()
        {
            Id = vm.Id,
            Area = vm.Area,
            Location = vm.Location,
            RoomNumber = vm.RoomNumber,
            BuildingType = vm.BuildingType,
            CreatedAt = vm.CreatedAt,
            ModifiedAt = vm.ModifiedAt,
            Files = vm.Files,
            Image = vm.Images
                .Select(x => new FileToDatabaseDto()
                {
                    Id = x.Id,
                    ImageTitle = x.ImageTitle,
                    ImageData = x.ImageData,
                    RealEstateId = x.RealEstateId
                }).ToArray()
        };

        var result = await _realEstateServices.Create(dto);

        if (result == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]

    public async Task<IActionResult> Delete(Guid id)
    {
        var realEstate = await _realEstateServices.DetailAsync(id);

        if (realEstate == null)
        {
            return NotFound();
        }

        ImageViewModel[] photos = await ShowImage(id);
        var vm = new RealEstateDeleteViewModel();

        vm.Id = realEstate.Id;
        vm.Area = realEstate.Area;
        vm.Location = realEstate.Location;
        vm.RoomNumber = realEstate.RoomNumber;
        vm.BuildingType = realEstate.BuildingType;
        vm.CreatedAt = realEstate.CreatedAt;
        vm.ModifiedAt = realEstate.ModifiedAt;
        vm.Images.AddRange(photos);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmation(Guid? id)
    {
        var realEstate = await _realEstateServices.Delete(id);

        if (realEstate == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]

    public async Task<IActionResult> Update(Guid id)
    {
        var realEstate = await _realEstateServices.DetailAsync(id);

        if (realEstate == null)
        {
            return NotFound();
        }

        ImageViewModel[] photos = await ShowImage(id);
        var vm = new RealEstateCreateUpdateViewModel();

        vm.Id = realEstate.Id;
        vm.Area = realEstate.Area;
        vm.Location = realEstate.Location;
        vm.RoomNumber = realEstate.RoomNumber;
        vm.BuildingType = realEstate.BuildingType;
        vm.CreatedAt = realEstate.CreatedAt;
        vm.ModifiedAt = realEstate.ModifiedAt;
        vm.Images.AddRange(photos);
        
        
        return View("CreateUpdate" ,vm);
    }

    [HttpPost]

    public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
    {
        var dto = new RealEstateDto()
        {
            Id = vm.Id,
            Area = vm.Area,
            Location = vm.Location,
            RoomNumber = vm.RoomNumber,
            BuildingType = vm.BuildingType,
            CreatedAt = vm.CreatedAt,
            ModifiedAt = vm.ModifiedAt,
            Files = vm.Files,
            Image = vm.Images
                .Select(x => new FileToDatabaseDto
                {
                    Id = x.Id,
                    ImageTitle = x.ImageTitle,
                    ImageData = x.ImageData,
                    RealEstateId = x.RealEstateId
                }).ToArray()
        };
        
        var result = await _realEstateServices.Update(dto);

        if (result == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return RedirectToAction(nameof(Index), vm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var realEstates = await _realEstateServices.DetailAsync(id);

        if (realEstates == null)
        {
            return NotFound();
        }

        ImageViewModel[] photos = await ShowImage(id);
        
        var vm = new RealEstateDetailsViewModel();
        
        vm.Id = realEstates.Id;
        vm.Area = realEstates.Area;
        vm.Location = realEstates.Location;
        vm.RoomNumber = realEstates.RoomNumber;
        vm.BuildingType = realEstates.BuildingType;
        vm.CreatedAt = realEstates.CreatedAt;
        vm.ModifiedAt = realEstates.ModifiedAt;
        vm.Image.AddRange(photos);

        return View(vm);
    }

    public async Task<ImageViewModel[]> ShowImage(Guid id)
    {
        var images = await _context.FileToDatabases
            .Where(x => x.RealEstateId == id)
            .Select(y => new ImageViewModel()
            {
                RealEstateId = y.Id,
                Id = y.Id,
                ImageData = y.ImageData,
                ImageTitle = y.ImageTitle,
                Images = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
            }).ToArrayAsync();
        return images;
    }
}