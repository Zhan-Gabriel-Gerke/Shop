namespace ShopTARgv24.Models.Kindergarten;

public class ImageViewModel
{
    public Guid Id { get; set; }
    public string? ImageTitle { get; set; }
    public byte[]? ImageData { get; set; }
    public string? Images {get; set;}
    public Guid? KindergartenId { get; set; }
}
