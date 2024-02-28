using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.ApplicationCore.Domain
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // E.g., "In Maintenance", "Operational"
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        // Entity Type to which this status is associated
        [Required]
        [StringLength(50)]
        public string Object { get; set; } // E.g., "Poste"
    }
}
