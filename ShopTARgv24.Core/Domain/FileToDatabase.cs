namespace ShopTARgv24.Core.Domain;

public class FileToDatabase
{
    public Guid Id { get; set; }
    public string? ImageTitle { get; set; }
    public byte[]? ImageData { get; set; }
    public Guid? KindergartenId { get; set; }
}