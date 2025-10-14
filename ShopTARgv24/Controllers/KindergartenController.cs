using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Models.Kindergarten;
using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARgv24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARgv24Context _context;
        private readonly IKindergartenServices _kindergartenServices;
        private readonly IFileServices _fileServices;

        public KindergartenController(
            ShopTARgv24Context context,
            IKindergartenServices kindergartenServices,
            IFileServices fileServices
        )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
            _fileServices = fileServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexView()
                {
                    KindergartenId = x.KindergartenId,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new KindergartenCreateUpdateViewModel();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                KindergartenId = vm.KindergartenId,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Images
                    .Select(x => new FileToDatabaseDto()
                    {
                        Id = x.Id,
                        ImageTitle = x.ImageTitle,
                        ImageData = x.ImageData,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);
            if (kindergarten == null)
            {
                return NotFound();
            }

            var photos = await ShowImage(id);

            var vm = new KindergartenCreateUpdateViewModel
            {
                KindergartenId = kindergarten.KindergartenId,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName,
                CreatedAt = kindergarten.CreatedAt,
                ModifiedAt = kindergarten.ModifiedAt,
            };
            vm.Images.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                KindergartenId = vm.KindergartenId,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Images
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.Id,
                        ImageTitle = x.ImageTitle,
                        ImageData = x.ImageData,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);
            if (kindergarten == null)
            {
                return NotFound();
            }

            var photos = await ShowImage(id);
            var vm = new KindergartenDetailsViewModel
            {
                KindergartenId = kindergarten.KindergartenId,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName,
                CreatedAt = kindergarten.CreatedAt,
                ModifiedAt = kindergarten.ModifiedAt,
            };
            vm.Image.AddRange(photos);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);
            if (kindergarten == null)
            {
                return NotFound();
            }
            
            var photos = await ShowImage(id);
            var vm = new KindergartenDeleteViewModel
            {
                KindergartenId = kindergarten.KindergartenId,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName,
                CreatedAt = kindergarten.CreatedAt,
                ModifiedAt = kindergarten.ModifiedAt,
            };
            vm.Images.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid kindergartenId)
        {
            var result = await _kindergartenServices.Delete(kindergartenId);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<ImageViewModel[]> ShowImage(Guid id)
        {
            var images = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new ImageViewModel()
                {
                    Id = y.Id, // Убедитесь, что ID файла передается
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Images = $"data:image/jpeg;base64,{Convert.ToBase64String(y.ImageData)}"
                }).ToArrayAsync();
            
            return images;
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = vm.Id
            };

            // ИСПРАВЛЕННЫЙ ВЫЗОВ: создаем массив из одного элемента
            var result = await _fileServices.RemoveImagesFromDatabase(new[] { dto });

            if (result == null)
            {
                // Можно добавить обработку ошибки, если нужно
                return RedirectToAction(nameof(Index));
            }
            
            // После удаления изображения лучше вернуться на страницу редактирования
            return RedirectToAction(nameof(Update), new { id = vm.KindergartenId });
        }
    }
}