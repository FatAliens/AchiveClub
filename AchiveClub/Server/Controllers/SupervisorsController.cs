using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AchiveClub.Server.Mappers;
using AchiveClub.Shared.DTO;

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
        public IEnumerable<SupervisorInfo> GetAll()
        {
            return _dbContext.Supervisors
                .Select(s => SupervisorToSupervisorInfoMapper.Map(s))
                .ToList();
        }

        [HttpPost]
        public ActionResult Post(SupervisorInfo supervisorInfo)
        {
            try
            {
                _dbContext.Supervisors.Add(SupervisorToSupervisorInfoMapper.Revert(supervisorInfo));
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(SupervisorInfo supervisorInfo)
        {
            try
            {
                _dbContext.Supervisors.Update(SupervisorToSupervisorInfoMapper.Revert(supervisorInfo));
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
