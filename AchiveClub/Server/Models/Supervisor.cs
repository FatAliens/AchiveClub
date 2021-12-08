using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Server.Models
{
    public class Supervisor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        public List<CompletedAchievement> CompletedAchivements { get; set; }

        public Supervisor()
        {
            CompletedAchivements = new List<CompletedAchievement>();
        }

        public override string ToString()
        {
            return $"[{Key}] {Name}";
        }
    }
}
