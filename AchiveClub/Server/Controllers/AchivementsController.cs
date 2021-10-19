using System;
using AchiveClub.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchivementsController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private LiteDbContext _dbContext;

        public AchivementsController(ILogger<UsersController> logger, LiteDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Achive> Get()
        {
            _logger.LogInformation("Get: AllAchivements");
            return _dbContext.Db.GetCollection<Achive>("Achivements").FindAll();
        }

        [HttpPost]
        public int Post(Achive achive)
        {
            _logger.LogInformation("Post: Achivements");
            return _dbContext.Db.GetCollection<Achive>("Achivements").Insert(achive);
        }

        [HttpPut]
        public bool Put(Achive achive)
        {
            _logger.LogInformation("Put: Achivements");
            return _dbContext.Db.GetCollection<Achive>("Achivements").Update(achive);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation("Delete: Achivements");
            return _dbContext.Db.GetCollection<Achive>("Achivements").Delete(id);
        }
    }
}
