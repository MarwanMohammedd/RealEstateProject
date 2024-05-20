using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaProject.Domain.Entities
{
    /// <summary>
    /// Number Of Rooms In Villa 
    /// </summary>
    [Table("VillaNumber")]
    public class VillaNumber
    {
        //DataBase Will Not Generate Identity Number
        [Key , DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Room ID")]
        public int VillaNumberID { get; set; }

        [ForeignKey("ID")]
        [Display(Name = "Villa ID")]
        public int VillaID { get; set; }
        public Villa Villa { get; set; }
        [Display(Name = "More Information")]
        public string? MoreInformation { get; set; }
    }
}