using System.ComponentModel.DataAnnotations;

namespace VillaProject.Presentation.ViewModels
{
    public class LogInViewModel{
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)] //hide what is type
        public string Password { get; set; }= null!;
        public bool RememberMe { get; set; }
        public string? RedirecURL { get; set; }
    }
}