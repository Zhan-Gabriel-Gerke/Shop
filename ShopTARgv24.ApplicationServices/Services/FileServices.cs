using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IHostEnvironment _webHost;

        public FileServices(
            ShopTARgv24Context context,
            IHostEnvironment webHost
        )
        {
            _context = context;
            _webHost = webHost;
        }

        public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var imageId = await _context.FileToDatabases
                .FirstOrDefaultAsync(x => x.KindergartenId == dto.KindergartenId);

            if (imageId != null)
            {
                _context.FileToDatabases.Remove(imageId);
                await _context.SaveChangesAsync();

                return imageId;
            }

            return null;
        }
        
        public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var imageId = await _context.FileToDatabases
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (imageId != null)
                {
                    _context.FileToDatabases.Remove(imageId);
                    await _context.SaveChangesAsync();
                }
            }
            return null;
        }

        public void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        var fileToDatabase = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            ImageData = target.ToArray(),
                            KindergartenId = domain.KindergartenId
                        };
                        _context.FileToDatabases.Add(fileToDatabase);
                    }
                }
            }
        }
    }
}