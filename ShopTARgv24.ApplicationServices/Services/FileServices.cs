using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            var removedFiles = new List<FileToDatabase>();
            foreach (var dto in dtos)
            {
                var file = await _context.FileToDatabases.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (file != null)
                {
                    _context.FileToDatabases.Remove(file);
                    removedFiles.Add(file);
                }
            }

            await _context.SaveChangesAsync();
            return removedFiles;
        }

        // Ниже приведены нереализованные методы из вашего интерфейса.
        // Я оставил их как есть, чтобы избежать ошибок компиляции.

        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            throw new System.NotImplementedException();
        }

        public Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            throw new System.NotImplementedException();
        }
    }
}