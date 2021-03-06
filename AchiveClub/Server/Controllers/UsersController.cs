using System;
using AchiveClub.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AchiveClub.Shared.DTO;
using AchiveClub.Server.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private ApplicationContext _dbContext;

        public UsersController(ILogger<UsersController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetAllUsers")]
        public IEnumerable<SmallUserInfo> GetAll()
        {
            return UserToSmallUserInfoMapper
                .Map(_dbContext.Users
                    .Include(u => u.Club)
                    .Include(u => u.CompletedAchivements)
                    .ThenInclude(a => a.Achive)
                    .ToList())
                .OrderByDescending(u => u.XPSum);
        }

        [HttpGet("{id}", Name = "GetOneUser")]
        public UserInfo GetOne(int id)
        {
            var user = UserToUserInfoMapper
                .Map(_dbContext.Users
                    .Where(u => u.Id == id)
                    .Include(u => u.Club)
                    .Include(u => u.CompletedAchivements)
                    .First(),
                    _dbContext.Achivements.ToList());
            return user;
        }

        [HttpPost]
        public ActionResult Post(UserInfo user)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(UserInfo user)
        {
            try
            {
                throw new NotImplementedException();
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
                _dbContext.Users.Remove(_dbContext.Users.Where(u => u.Id == id).First());
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