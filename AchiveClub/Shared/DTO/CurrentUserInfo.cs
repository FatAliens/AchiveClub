using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClub.Shared.DTO
{
    public class CurrentUserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public List<AchiveInfo> Achivements { get; set; }
    }
}
