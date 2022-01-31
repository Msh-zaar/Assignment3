using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    [Table("Character")]
    public class Character
    {
        // Pk
        public int Id { get; set; }
        // Fields
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        [Required]
        [MaxLength(50)]
        public string Gender { get; set; }
        [MaxLength(255)]
        public string Photo { get; set; }
        // Relationships
        public ICollection<Movie> Movies { get; set; }

    }
}
