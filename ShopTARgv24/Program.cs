using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using ShopTARgv24.Hubs;

namespace ShopTARgv24
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
            builder.Services.AddScoped<IFileServices, FileServices>();
            builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
            builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
            builder.Services.AddScoped<IChucknorrisServices, ChucknorrisServices>();
            builder.Services.AddScoped<ICocktailServices, CocktailServices>();
            builder.Services.AddScoped<IEmailService, EmailServices>();
            builder.Services.AddHttpClient<ICocktailServices, CocktailServices>();
            builder.Services.AddSignalR();

            builder.Services.AddDbContext<ShopTARgv24Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                })
                .AddEntityFrameworkStores<ShopTARgv24Context>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation");
            
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseStaticFiles();

            app.MapControllers().RequireAuthorization();
            
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapHub<ChatHub>("/chatHub");
            app.Run();
        }
    }
}
