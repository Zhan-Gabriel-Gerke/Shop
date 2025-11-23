using Microsoft.Extensions.FileProviders;

namespace ShopTARgv24.Kindergarten.Macros;

public interface IMacros
{
    public string ApplicationName { get; set; }
    public IFileProvider ContentRootFileProvider { get; set; }
    public string ContentRootPath { get; set; }
    public string EnvironmentName { get; set; }
}