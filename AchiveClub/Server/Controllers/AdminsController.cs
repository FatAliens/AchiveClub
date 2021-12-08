using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AchiveClub.Server.Mappers;
using AchiveClub.Shared.DTO;

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

        [HttpGet]
        public IEnumerable<AdminInfo> GetAll()
        {
            return _dbContext.Admins
                .Select(a => AdminToAdminInfoMapper.Map(a))
                .ToList();
        }

        [HttpPost]
        public ActionResult Post(AdminInfo adminInfo)
        {
            try
            {
                _dbContext.Admins.Add(AdminToAdminInfoMapper.Revert(adminInfo));
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(AdminInfo adminInfo)
        {
            try
            {
                _dbContext.Admins.Update(AdminToAdminInfoMapper.Revert(adminInfo));
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
                _dbContext.Admins.Remove(_dbContext.Admins.Where(u => u.Id == id).First());
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
