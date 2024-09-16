using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VillaProject.Presentation.ViewModels{
    public class SignUpViewModel{
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)] //hide what is type
        public string Password { get; set; }= null!;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password is not match with confirmpassword")] // compare with password
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public string Name { get; set; }= null!;

        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; } = null!;
         
        public string? RedirecURL { get; set; }
        public string? Role { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}