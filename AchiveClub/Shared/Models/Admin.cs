using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Shared.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        public List<CompletedAchive> CompletedAchivements { get; set; }

        public Admin()
        {
            CompletedAchivements = new List<CompletedAchive>();
        }

        public override string ToString()
        {
            return $"[{Key}] {Name}";
        }
    }
}
