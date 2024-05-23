using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace VillaProject.Domain.Entities
{
    public class Villa
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string VillaName { get; set; } = null!;
        [StringLength(250)]
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}