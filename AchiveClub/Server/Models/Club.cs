using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AchiveClub.Server.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string LogoURL { get; set; }
        public List<User> Users { get; set; }

        public Club()
        {
            Users = new List<User>();
        }
    }
}
