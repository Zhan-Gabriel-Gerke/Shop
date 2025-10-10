using ShopTARgv24.Core.Domain;
using Microsoft.AspNetCore.Http;
namespace ShopTARgv24.Core.Dto;

public class KindergartenDto
{
    public Guid? KindergartenId { get; set; }
    public string? GroupName { get; set; }
    public int? ChildrenCount { get; set; }
    public string? KindergartenName { get; set; }
    public string? TeacherName { get; set; }
    public List<IFormFile> Files { get; set; }
    public IEnumerable<FileToDatabaseDto> Image { get; set; }
        = new List<FileToDatabaseDto>();
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}