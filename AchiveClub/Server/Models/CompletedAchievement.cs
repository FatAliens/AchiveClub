using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Server.Models
{
    public class CompletedAchievement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Admin))]
        public int SupervisorRefId { get; set; }       
        public Supervisor Supervisor { get; set; }

        [ForeignKey(nameof(User))]
        public int UserRefId { get; set; }  
        public User User { get; set; }

        [ForeignKey(nameof(Achive))]
        public int AchiveRefId { get; set; }
        public Achievement Achive { get; set; }

        [Required]
        public DateTime DateOfCompletion { get; set; }
    }
}
