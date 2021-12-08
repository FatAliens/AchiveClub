using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClub.Shared.DTO
{
    public class CompleteAchiveParams
    {
        public int AchiveId { get; set; }
        public int UserId { get; set; }
        public string SupervisorKey { get; set; }
    }
}
