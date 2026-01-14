using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ShopTARgv24.Data
{
    public class ShopTARgv24Context : IdentityDbContext<ApplicationUser>
    {
        public ShopTARgv24Context(DbContextOptions<ShopTARgv24Context> options) 
        : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }
    }
}
