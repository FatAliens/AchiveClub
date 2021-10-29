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

        [HttpPut("{adminKey}", Name = "UpdateUser")]
        public ActionResult Put(User user, string adminKey)
        {
            _logger.LogInformation("Put: Users");
            if (_dbContext.Db.GetCollection<Admin>("Admins").FindOne(a => a.Key == adminKey) != null)
            {
                _logger.LogInformation("Put: Admin Key Valid!");
                return _dbContext.Db.GetCollection<User>("Users").Update(user) ? new OkResult() : new BadRequestResult();
            }
            else
            {
                _logger.LogInformation("Put: Admin Key invalid!");
                return new BadRequestResult();
            }
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation("Delete: Users");
            return _dbContext.Db.GetCollection<User>("Users").Delete(id);
        }
    }
}
