using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VillaProject.Domain.Entities;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser> // Specify that IdentityDBCOntext work with ApplicationUser class 
{
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    public DbSet<Amenity> Amenitys { get; set; }
    public ApplicationDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
    }
}