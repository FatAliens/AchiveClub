using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;

namespace AchiveClub.Server.Mappers
{
    public class ClubToClubInfoMapper
    {

        static public ClubInfo Map(Club club)
        {
            return new ClubInfo
            {
                Id = club.Id,
                Title = club.Title,
                Description = club.Description,
                Address = club.Address,
                LogoURL = club.LogoURL,
                Users = UserToSmallUserInfoMapper.Map(club.Users)
            };
        }

        
    }
}
