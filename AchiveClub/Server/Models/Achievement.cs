using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Server.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Xp { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string LogoURL { get; set; }

        public List<CompletedAchievement> CompletedAchivements { get; set; }

        public Achievement()
        {
            CompletedAchivements = new List<CompletedAchievement>();
        }

        public override string ToString()
        {
            return $"[{Xp} Xp] {Title}\n" +
                $"{Description}\n" +
                $"Achivement Logo: [{LogoURL}]";
        }
    }
}
