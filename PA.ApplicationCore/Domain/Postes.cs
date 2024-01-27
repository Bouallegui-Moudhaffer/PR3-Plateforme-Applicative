using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.ApplicationCore.Domain
{
    public class Postes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostesId { get; set; }
        [Required]
        [StringLength(50)]
        public string Ref { get; set; } // Name or Identifier for the workstation
        [Required]
        [StringLength(100)]
        public string Configuration { get; set; }
        [DataType(DataType.Date)]
        public DateTime derniereMaintenance { get; set; }

        // Foreign Key
        public int SallesId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        // Navigation properties
        public virtual Salles Salle { get; set; }
        public virtual Status Status { get; set; }
        public virtual Types Type { get; set; }
    }
}
