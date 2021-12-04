using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    static public class UserToUserInfoMapper
    {
        static public UserInfo Map(User user, List<Achievement> achievements)
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
                LastName = user.LastName,
                Avatar = user.Avatar,
                Achivements = achivementsInfo,
                ClubTitle = user.Club.Title,
                ClubAddress = user.Club.Address,
                ClubIcon = user.Club.LogoURL
            };

            return userInfo;
        }
    }
}
