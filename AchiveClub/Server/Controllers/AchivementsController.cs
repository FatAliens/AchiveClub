using System;
using AchiveClub.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
            if (!_dbContext.Admins.Where(a => a.Key == completeAchiveParams.AdminKey).Any())
            {
                return BadRequest("Admin key invalid!");
            }

            var completedAchievament = new CompletedAchievement()
            {
                User = _dbContext.Users.First(u => u.Id == completeAchiveParams.UserId),
                Achive = _dbContext.Achivements.First(a => a.Id == completeAchiveParams.AchiveId),
                Admin = _dbContext.Admins.First(a => a.Key == completeAchiveParams.AdminKey),
                DateOfCompletion = DateTime.Now
            };

            _dbContext.CompletedAchivements.Add(completedAchievament);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<Achievement> Get()
        {
            return _dbContext.Achivements.ToList();
        }

        [HttpPost]
        public ActionResult Post(Achievement achive)
        {
            try
            {
                _dbContext.Achivements.Add(achive);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Achievement achive)
        {
            try
            {
                _dbContext.Achivements.Update(achive);
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
