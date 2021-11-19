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
    public class AdminsController : ControllerBase
    {
        private readonly ILogger<AdminsController> _logger;
        private ApplicationContext _dbContext;

        public AdminsController(ILogger<AdminsController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpGet("{key}", Name = "ConfirmKey")]
        public bool Get(string key)
        {
            if (_dbContext.Admins.Where(a => a.Key == key).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            return _dbContext.Admins.ToList();
        }

        [HttpPost]
        public ActionResult Post(Admin admin)
        {
            try
            {
                _dbContext.Admins.Add(admin);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return new OkResult();
        }

        [HttpPut]
        public ActionResult Put(Admin admin)
        {
            try
            {
                _dbContext.Admins.Update(admin);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _dbContext.Admins.Remove(_dbContext.Admins.Where(u => u.Id == id).First());
                _dbContext.SaveChanges();
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return new OkResult();
        }
    }
}
