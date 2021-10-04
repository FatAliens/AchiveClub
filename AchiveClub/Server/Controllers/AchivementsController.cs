using System;
using AchiveClub.Shared;
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
            return _dbContext.Db.GetCollection<Achive>("Achivements").FindAll();
        }

        [HttpGet(template:"{id}")]
        public Achive Get(int id)
        {
            return _dbContext.Db.GetCollection<Achive>("Achivements").FindOne(a=>a.Id==id);
        }

        [HttpPost]
        public int Post(Achive achive)
        {
            return _dbContext.Db.GetCollection<Achive>("Achivements").Insert(achive);
        }

        [HttpPut]
        public bool Put(Achive achive)
        {
            return _dbContext.Db.GetCollection<Achive>("Achivements").Update(achive);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _dbContext.Db.GetCollection<Achive>("Achivements").Delete(id);
        }
    }
}
