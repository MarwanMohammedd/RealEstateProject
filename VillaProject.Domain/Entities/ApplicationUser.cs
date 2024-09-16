using Microsoft.AspNetCore.Identity;
namespace VillaProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //ADD TO COLUMNS IN ASPNETUSER TABLE IN DATABASE
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
