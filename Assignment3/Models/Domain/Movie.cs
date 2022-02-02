using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    [Table("Movie")]
    public class Movie
    {
        // Pk
        public int Id { get; set; }
        // Fields
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; }
        [Required]
        [MaxLength(4)]
        public int ReleaseYear { get; set; }
        [Required]
        [MaxLength(50)]
        public string Director { get; set; }
        [MaxLength(255)]
        public string Picture { get; set; }
        [MaxLength(255)]
        public string Trailer { get; set; }
        // Relationships
        public ICollection<Character> Characters { get; set; }
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
    }
}
