using System;
using AchiveClub.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

        [HttpGet]
        public IEnumerable<User> Get()
        {
            _logger.LogInformation("Get: AllUsers");
            return _dbContext.Db.GetCollection<User>("Users").FindAll();
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

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation("Delete: Users");
            return _dbContext.Db.GetCollection<User>("Users").Delete(id);
        }
    }
}
