using System.Collections.Generic;

namespace Assignment3.Models.DTOs.Movie
{
    public class MovieReadDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        // Relationships
        public List<int> Characters { get; set; }
        public int FranchiseId { get; set; }
    }
}
