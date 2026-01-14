namespace ShopTARgv24.Models;

public class EmailViewModel
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public IFormFileCollection Attachments { get; set; }

}