using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchiveClub.Shared.Models
{
    public class CompletedAchive
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Admin))]
        public int AdminRefId { get; set; }       
        public Admin Admin { get; set; }

        [ForeignKey(nameof(User))]
        public int UserRefId { get; set; }  
        public User User { get; set; }

        [ForeignKey(nameof(Achive))]
        public int AchiveRefId { get; set; }
        public Achive Achive { get; set; }

        [Required]
        public DateTime DateOfCompletion { get; set; }
    }
}
