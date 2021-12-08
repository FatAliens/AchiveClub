using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClub.Shared.DTO
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string ClubTitle { get; set; }
        public string ClubAddress { get; set; }
        public string ClubIcon { get; set; }
        public List<UserAchiveInfo> Achivements { get; set; }
    }
}
