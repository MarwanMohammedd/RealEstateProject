using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaProject.Domain.Entities
{
 public class Villa
{
    [Key]
    public int ID { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Name")]
    public string VillaName { get; set; } = null!;
    [StringLength(250)]
    public string? Description { get; set; }
    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public int Occupancy { get; set; }
    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}   
}