using System.Collections.Generic;

namespace Assignment3.Models.DTOs.Character
{
    public class CharacterReadDTO
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public List<int> Movies { get; set; }
    }
}
