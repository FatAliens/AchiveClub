using System;
using AchiveClub.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private LiteDbContext _dbContext;

        public UsersController(ILogger<UsersController> logger, LiteDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("userId" , Name = "GetOne")]
        public User GetOne(int userId)
        {
            _logger.LogInformation("Get: AllUsers");
            return _dbContext.Db.GetCollection<User>("Users").Include(u => u.CompletedAchivements).FindById(userId);
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<User> GetAll()
        {
            _logger.LogInformation("Get: AllUsers");
            return _dbContext.Db.GetCollection<User>("Users").Include(u=>u.CompletedAchivements).FindAll();
        }

        [HttpPost]
        public int Post(User user)
        {
            _logger.LogInformation("Post: Users");
            return _dbContext.Db.GetCollection<User>("Users").Insert(user);
        }

        [HttpPut]
        public bool Put(User user)
        {
            _logger.LogInformation("Put: Users");
            return _dbContext.Db.GetCollection<User>("Users").Update(user);
        }

        [HttpPut("{userId}&{achiveId}&{adminKey}" ,Name = "AddCompletedAchiveToUser")]
        public bool Put(int userId, int achiveId, string adminKey)
        {
            _logger.LogInformation("Put: AddCompletedAchiveToUser");
            User user = _dbContext.Db.GetCollection<User>("Users").FindById(userId);
            Achive achive = _dbContext.Db.GetCollection<Achive>("Achivements").FindById(achiveId);
            if(user.CompletedAchivements.Count(a=>a.Id==achive.Id)==0)
            {
                user.CompletedAchivements.Add(achive);
            }
            else
            {
                return false;
            }
            return _dbContext.Db.GetCollection<User>("Users").Update(user);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation("Delete: Users");
            return _dbContext.Db.GetCollection<User>("Users").Delete(id);
        }
    }
}
