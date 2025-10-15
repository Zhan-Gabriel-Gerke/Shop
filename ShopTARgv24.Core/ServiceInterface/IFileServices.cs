using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IFileServices
    {
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
        Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
    }
}