using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    public class UserToSmallUserInfoMapper
    {
        static public List<SmallUserInfo> Map(List<User> users)
        {
            var usersInfo = new List<SmallUserInfo>();

            foreach (var user in users)
            {
                usersInfo.Add(Map(user));
            }

            return usersInfo;
        }

        static public SmallUserInfo Map(User user)
        {

            var userInfo = new SmallUserInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                XPSum = user.CompletedAchivements.Sum(a => a.Achive.Xp),
                ClubIcon = user.Club.LogoURL
            };

            return userInfo;
        }
    }
}
