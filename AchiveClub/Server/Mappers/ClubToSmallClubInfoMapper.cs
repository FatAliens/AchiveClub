using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    public class ClubToSmallClubInfoMapper
    {
        public static SmallClubInfo Map(Club club)
        {
            var smallInfo = new SmallClubInfo
            {
                Id = club.Id,
                Title = club.Title,
                LogoURL = club.LogoURL
            };

            if (club.Users.Count == 0)
            {
                smallInfo.AvgXP = 0;
            }
            else
            {
                smallInfo.AvgXP =
                    (int)club.Users
                        .Average(u => u.CompletedAchivements
                        .Sum(a => a.Achive.Xp));
            }

            return smallInfo;
        }

        public static List<SmallClubInfo> Map(List<Club> clubs)
        {
            return clubs.Select(c => Map(c)).ToList();
        }
    }
}
