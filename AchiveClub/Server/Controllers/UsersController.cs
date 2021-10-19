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

        [HttpGet("{adminKey}")]
        public IEnumerable<User> Get(string adminKey)
        {
            return _dbContext.Db.GetCollection<User>("Users").FindAll();
        }

        [HttpGet("{email}&{adminKey}")]
        public User Get(string email, string adminKey)
        {
            return _dbContext.Db.GetCollection<User>("Users").FindOne(u=>u.Email==email);
        }

        [HttpPost("{adminKey}")]
        public int Post(User user)
        {
            return _dbContext.Db.GetCollection<User>("Users").Insert(user);
        }

        [HttpPut("{adminKey}")]
        public bool Put(User user)
        {
            return _dbContext.Db.GetCollection<User>("Users").Update(user);
        }

        [HttpDelete("{id}&{adminKey}")]
        public bool Delete(int id)
        {
            return _dbContext.Db.GetCollection<User>("Users").Delete(id);
        }
    }
}
