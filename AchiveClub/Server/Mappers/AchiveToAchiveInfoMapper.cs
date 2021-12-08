using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;

namespace AchiveClub.Server.Mappers
{
    static public class AchiveToAchiveInfoMapper
    {
        static public AchiveInfo Map(Achievement achive)
        {
            return new AchiveInfo
            {
                Id = achive.Id,
                Title = achive.Title,
                Description = achive.Description,
                Xp = achive.Xp,
                LogoURL = achive.LogoURL
            };
        }

        static public Achievement Revert(AchiveInfo achiveInfo)
        {
            return new Achievement
            {
                Id = achiveInfo.Id,
                Title = achiveInfo.Title,
                Description = achiveInfo.Description,
                Xp = achiveInfo.Xp,
                LogoURL = achiveInfo.LogoURL
            };
        }
    }
}
