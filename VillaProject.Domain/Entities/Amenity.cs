using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VillaProject.Domain.Entities
{
    public class Amenity
    {
        public int ID { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; } = null!;
        [StringLength(250)]
        public string? Description { get; set; }

        [ForeignKey("ID")]
        [Display(Name ="Villa")]
        public int VillaID { get; set; }
        [ValidateNever]
        public Villa Villa { get; set; } = null!;
    }
}