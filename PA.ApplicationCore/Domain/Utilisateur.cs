using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PA.ApplicationCore.Domain
{
    public class Utilisateur
    {
        [Key]
        public int UtilisateurId { get; set; } // Primary Key

        [Required]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(UserRoles))]
        [Column(TypeName = "nvarchar(24)")]
        public UserRoles Role { get; set; }

        [Required]
        [Category("Security")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        public int StatusId { get; set; }

        // Navigation properties
        public virtual Status Status { get; set; }
    }
}
