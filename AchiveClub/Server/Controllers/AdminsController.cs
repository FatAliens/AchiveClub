using System;
using AchiveClub.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ILogger<AdminsController> _logger;
        private LiteDbContext _dbContext;

        public AdminsController(ILogger<AdminsController> logger, LiteDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            _logger.LogInformation("Get: AllAdmins");
            return _dbContext.Db.GetCollection<Admin>("Admins").FindAll();
        }

        [HttpGet("{key}" ,Name = "ConfirmKey")]
        public bool Get(string key)
        {
            _logger.LogInformation("Get: TryConfirm");
            return _dbContext.Db.GetCollection<Admin>("Admins").Count(admin => admin.Key == key) > 0;
        }

        [HttpPost]
        public int Post(Admin admin)
        {
            _logger.LogInformation("Post: Admins");
            return _dbContext.Db.GetCollection<Admin>("Admins").Insert(admin);
        }

        [HttpPut]
        public bool Put(Admin admin)
        {
            _logger.LogInformation("Put: Admins");
            return _dbContext.Db.GetCollection<Admin>("Admins").Update(admin);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation("Delete: Admins");
            return _dbContext.Db.GetCollection<Admin>("Admins").Delete(id);
        }
    }
}
