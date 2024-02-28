using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.ApplicationCore.Domain
{
    public class Salles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SallesId { get; set; }
        public int Capacite { get; set; }
        // Foreign Key
        public int EtablissementId { get; set; }
        public int StatusId { get; set; }
        // Navigation properties
        public virtual ICollection<Postes>? Postes { get; set; }
    }
}
