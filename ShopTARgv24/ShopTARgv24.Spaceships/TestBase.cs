using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Spaceships.Macros;
using ShopTARgv24.Spaceships.Macros.Mock;

namespace ShopTARgv24.Spaceships;

public abstract class TestBase
{
    protected IServiceProvider serviceProvider { get; set; }

    protected TestBase()
    {
        var services = new ServiceCollection();
        SetupServices(services);
        serviceProvider = services.BuildServiceProvider();
    }

    public virtual void SetupServices(IServiceCollection services)
    {
        // Регистрация сервисов приложения
        services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
        services.AddScoped<IRealEstateServices, RealEstateServices>();
        services.AddScoped<IFileServices, FileServices>();

        // Макет окружения (IHostEnvironment)
        services.AddScoped<IHostEnvironment, MockHostEnviroment>();

        // Конфигурация InMemory базы данных
        services.AddDbContext<ShopTARgv24Context>(x =>
        {
            x.UseInMemoryDatabase("TestDb");
            x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        RegisterMacros(services);
    }

    private void RegisterMacros(IServiceCollection services)
    {
        var macroBaseType = typeof(IMacros);

        var macros = macroBaseType.Assembly.GetTypes()
            .Where(t => macroBaseType.IsAssignableFrom(t)
                        && !t.IsInterface && !t.IsAbstract);

        foreach (var macro in macros)
        {
            services.AddTransient(macro);
        }
    }

    protected T Svc<T>()
    {
        return serviceProvider.GetService<T>();
    }
}