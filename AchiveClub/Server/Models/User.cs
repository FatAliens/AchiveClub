using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Avatar { get; set; }

        public List<CompletedAchievement> CompletedAchivements { get; set; }

        public User()
        {
            CompletedAchivements = new List<CompletedAchievement>();
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}\n" +
                $"{Email}\n" +
                $"[{Password}]";
        }
    }
}
