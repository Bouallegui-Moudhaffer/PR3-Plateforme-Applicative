using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.ApplicationCore.Domain
{
    public class Etablissement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EtablissementId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nom { get; set; }
        [Required]
        [StringLength(100)]
        public string Adresse { get; set; }
        [Required]
        [RegularExpression(@"^\+?(\d{1,3})?[-. (]?(\d{3})[-. )]?(\d{3})[-. ]?(\d{4})$",
            ErrorMessage = "Invalid phone number format")]
        public string Telephone { get; set; }
        public int StatusId { get; set; }

        // Relations
        public ICollection<Salles> Salles { get; set; }
    }
}
