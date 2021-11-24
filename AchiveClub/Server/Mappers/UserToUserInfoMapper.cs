using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    static public class UserToUserInfoMapper
    {
        static public List<UserInfo> UsersToUserInfo(List<User> users, List<Achievement> achievements)
        {
            var usersInfo = new List<UserInfo>();

            foreach (var user in users)
            {
                usersInfo.Add(UserToUserInfo(user, achievements));
            }

            return usersInfo;
        }

        static public UserInfo UserToUserInfo(User user, List<Achievement> achievements)
        {
            var achivementsInfo = new List<AchiveInfo>();

            foreach (var achive in achievements)
            {
                achivementsInfo.Add(new AchiveInfo()
                {
                    Id = achive.Id,
                    Title = achive.Title,
                    Description = achive.Description,
                    Xp = achive.Xp,
                    LogoURL = achive.LogoURL,
                    Completed = user.CompletedAchivements.Where(a => a.AchiveRefId == achive.Id).Any()
                });
            }

            var userInfo = new UserInfo()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Achivements = achivementsInfo
            };

            return userInfo;
        }
    }
}
