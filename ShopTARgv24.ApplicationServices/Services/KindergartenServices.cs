using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices(
            ShopTARgv24Context context,
            IFileServices fileServices
        )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            var kindergarten = new Kindergarten
            {
                KindergartenId = Guid.NewGuid(),
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                TeacherName = dto.TeacherName,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            if (dto.Files != null && dto.Files.Count > 0)
            {
                _fileServices.UploadFilesToDatabase(dto, kindergarten);
            }

            await _context.Kindergartens.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .Include(k => k.Files) // Включаем связанные файлы для детального просмотра
                .FirstOrDefaultAsync(x => x.KindergartenId == id);

            return result;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            // 1. Находим существующую запись в базе данных
            var domain = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.KindergartenId == dto.KindergartenId);

            if (domain == null)
            {
                return null;
            }

            // 2. Обновляем её свойства
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergartenName = dto.KindergartenName;
            domain.TeacherName = dto.TeacherName;
            domain.ModifiedAt = DateTime.Now;

            // 3. Загружаем новые файлы, если они были добавлены
            if (dto.Files != null && dto.Files.Count > 0)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            // 4. Сохраняем все изменения одной транзакцией
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.KindergartenId == id);

            var images = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    KindergartenId = y.KindergartenId
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(images);

            _context.Kindergartens.Remove(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }
    }
}