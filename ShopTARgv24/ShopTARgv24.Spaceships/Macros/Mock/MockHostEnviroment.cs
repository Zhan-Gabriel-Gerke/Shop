using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace ShopTARgv24.Spaceships.Macros.Mock;

public class MockHostEnviroment : IHostEnvironment
{
    public string ApplicationName { get; set; }
    public IFileProvider ContentRootFileProvider { get; set; }
    public string ContentRootPath { get; set; }
    public string EnvironmentName { get; set; }
}