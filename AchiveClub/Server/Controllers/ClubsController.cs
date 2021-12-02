using AchiveClub.Server.Mappers;
using AchiveClub.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly ILogger<ClubsController> _logger;
        private ApplicationContext _dbContext;

        public ClubsController(ILogger<ClubsController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetAllClubs")]
        public IEnumerable<SmallClubInfo> GetAll()
        {
            return ClubToSmallClubInfoMapper
                .Map(_dbContext.Clubs
                    .Include(c=>c.Users)
                    .ThenInclude(u=>u.CompletedAchivements)
                    .ThenInclude(a=>a.Achive)
                    .ToList()).OrderByDescending(c=>c.AvgXP);
        }

        [HttpGet("{id}", Name = "GetOneClub")]
        public ClubInfo GetOne(int id)
        {
            return ClubToClubInfoMapper
                .Map(_dbContext.Clubs
                    .Where(c => c.Id == id)
                    .Include(c => c.Users)
                    .First());
        }
    }
}
