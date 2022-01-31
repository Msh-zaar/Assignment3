using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    [Table("Franchise")]
    public class Franchise
    {
        // Pk
        public int Id { get; set; }
        // Fields
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        // Relationships
        public ICollection<Movie> Movies { get; set; }
    }
}
