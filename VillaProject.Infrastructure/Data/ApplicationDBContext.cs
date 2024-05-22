using System.Data;
using Microsoft.EntityFrameworkCore;
using VillaProject.Domain.Entities;

public class ApplicationDBContext : DbContext
{
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    public ApplicationDBContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }

}