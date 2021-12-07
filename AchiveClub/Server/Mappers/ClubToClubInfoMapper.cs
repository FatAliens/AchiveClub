using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    public class ClubToClubInfoMapper
    {

        static public ClubInfo Map(Club club, int position)
        {
            return new ClubInfo
            {
                Id = club.Id,
                Position = position,
                Title = club.Title,
                Description = club.Description,
                Address = club.Address,
                LogoURL = club.LogoURL,
                Users = UserToSmallUserInfoMapper.Map(club.Users.Take(3).ToList())
            };
        }

        
    }
}
