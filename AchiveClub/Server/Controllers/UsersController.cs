using System;
using AchiveClub.Shared;
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
            return _dbContext.Db.GetCollection<User>("Users").FindAll();
        }

        [HttpGet("{email}")]
        public User Get(string email)
        {
            return _dbContext.Db.GetCollection<User>("Users").FindOne(u=>u.Email==email);
        }

        [HttpPost]
        public int Post(User user)
        {
            return _dbContext.Db.GetCollection<User>("Users").Insert(user);
        }

        [HttpPut]
        public bool Put(User user)
        {
            return _dbContext.Db.GetCollection<User>("Users").Update(user);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _dbContext.Db.GetCollection<User>("Users").Delete(id);
        }
    }
}
