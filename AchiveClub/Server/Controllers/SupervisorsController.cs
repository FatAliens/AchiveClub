using System;
using AchiveClub.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorsController : ControllerBase
    {
        private readonly ILogger<SupervisorsController> _logger;
        private ApplicationContext _dbContext;

        public SupervisorsController(ILogger<SupervisorsController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Supervisor> Get()
        {
            return _dbContext.Supervisors.ToList();
        }

        [HttpPost]
        public ActionResult Post(Supervisor supervisor)
        {
            try
            {
                _dbContext.Supervisors.Add(supervisor);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Supervisor supervisor)
        {
            try
            {
                _dbContext.Supervisors.Update(supervisor);
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
                _dbContext.Supervisors.Remove(_dbContext.Supervisors.Where(u => u.Id == id).First());
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
