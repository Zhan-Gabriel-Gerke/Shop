using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);

        // Методы для Kindergarten
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
        Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
    }
}