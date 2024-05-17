using System.Data;
using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext : DbContext
{
    public DbSet<Villa> Villas { get; set; }
    public ApplicationDBContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                ID = 1,
                VillaName = "VillaNameOne",
                Price = 19000.50M,
                Occupancy = 4,
                ImageUrl = "Https://placeholder.com",
                Description = "VillaNameOne and 4 rooms and bathroom",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.MinValue
            }
    ,
      new Villa
      {
          ID = 2,
          VillaName = "VillaNameTwo",
          Price = 2500.50M,
          Occupancy = 1,
          ImageUrl = "Https://placeholder.com",
          Description = "VillaNameTwo and 1 rooms",
          CreatedDate = DateTime.Now,
          UpdatedDate = DateTime.MinValue
      }
    ,
       new Villa
       {
           ID = 3,
           VillaName = "VillaNameTwo",
           Price = 5000.57M,
           Occupancy = 2,
           ImageUrl = "Https://placeholder.com",
           Description = "VillaNameThree and 2 rooms",
           CreatedDate = DateTime.Now,
           UpdatedDate = DateTime.MinValue
       }
        );
    }

}