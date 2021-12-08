using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    public class AdminToAdminInfoMapper
    {

        static public AdminInfo Map(Admin admin)
        {
            return new AdminInfo
            {
                Id = admin.Id,
                Name = admin.Name,
                Password = admin.Password
            };
        }

        static public Admin Revert(AdminInfo adminInfo)
        {
            return new Admin
            {
                Id = adminInfo.Id,
                Name = adminInfo.Name,
                Password = adminInfo.Password
            };
        }
    }
}
