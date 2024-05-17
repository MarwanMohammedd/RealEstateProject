using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext : DbContext
{
    public DbSet<Villa> Villas { get; set; }
    public ApplicationDBContext(DbContextOptions options) : base(options) { }
}