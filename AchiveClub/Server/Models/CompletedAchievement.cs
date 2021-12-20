using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Server.Models
{
    public class CompletedAchievement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Supervisor))]
        public int SupervisorId { get; set; }       
        public Supervisor Supervisor { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }  
        public User User { get; set; }

        [ForeignKey(nameof(Achive))]
        public int AchiveId { get; set; }
        public Achievement Achive { get; set; }

        [Required]
        public DateTime DateOfCompletion { get; set; }
    }
}
