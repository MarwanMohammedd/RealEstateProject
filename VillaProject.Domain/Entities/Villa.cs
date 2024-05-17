using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Villa
{
    [Key]
    public int ID { get; set; }
    [Required]
    [StringLength(50)]
    public string VillaName { get; set; } = null!;
    [StringLength(250)]
    public string? Description { get; set; }
    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public int Occupancy { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}