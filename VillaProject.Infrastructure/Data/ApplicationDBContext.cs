using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VillaProject.Domain.Entities;

public class ApplicationDBContext : IdentityDbContext
{
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    public DbSet<Amenity> Amenitys { get; set; }
    public ApplicationDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
    }
}