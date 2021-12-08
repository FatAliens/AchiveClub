using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AchiveClub.Server.Models;
using AchiveClub.Server.Mappers;
using AchiveClub.Shared.DTO;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchivementsController : ControllerBase
    {
        private readonly ILogger<AchivementsController> _logger;
        private ApplicationContext _dbContext;

        public AchivementsController(ILogger<AchivementsController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("complete", Name = "CompleteAchievement")]
        public ActionResult CompleteAchievement(CompleteAchiveParams completeAchiveParams)
        {
            if (!_dbContext.Achivements.Where(a => a.Id == completeAchiveParams.AchiveId).Any())
            {
                return BadRequest("Achivement not found!");
            }
            if (!_dbContext.Users.Where(u => u.Id == completeAchiveParams.UserId).Any())
            {
                return BadRequest("User not found!");
            }
            if (!_dbContext.Supervisors.Where(a => a.Key == completeAchiveParams.SupervisorKey).Any())
            {
                return BadRequest("Admin key invalid!");
            }

            var completedAchievament = new CompletedAchievement()
            {
                User = _dbContext.Users.First(u => u.Id == completeAchiveParams.UserId),
                Achive = _dbContext.Achivements.First(a => a.Id == completeAchiveParams.AchiveId),
                Supervisor = _dbContext.Supervisors.First(a => a.Key == completeAchiveParams.SupervisorKey),
                DateOfCompletion = DateTime.Now
            };

            _dbContext.CompletedAchivements.Add(completedAchievament);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<AchiveInfo> GetAll()
        {
            return _dbContext.Achivements
                .Select(a => AchiveToAchiveInfoMapper.Map(a))
                .ToList();
        }

        [HttpPost]
        public ActionResult Post(AchiveInfo achiveInfo)
        {
            try
            {
                _dbContext.Achivements.Add(AchiveToAchiveInfoMapper.Revert(achiveInfo));
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(AchiveInfo achiveInfo)
        {
            try
            {
                _dbContext.Achivements.Update(AchiveToAchiveInfoMapper.Revert(achiveInfo));
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _dbContext.Achivements.Remove(_dbContext.Achivements.Where(u => u.Id == id).First());
                _dbContext.SaveChanges();
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}
