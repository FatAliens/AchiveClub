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

        [HttpGet(Name = "GetAll")]
        public IEnumerable<UserInfo> GetAll()
        {
            return UserToUserInfoMapper.UsersToUserInfo(_dbContext.Users.ToList(), _dbContext.Achivements.ToList());
        }

        [HttpGet("{id}", Name = "GetOne")]
        public UserInfo GetOne(int id)
        {
            var user =  UserToUserInfoMapper.UserToUserInfo(_dbContext.Users.Where(u => u.Id == id).Include(u=>u.CompletedAchivements).First(), _dbContext.Achivements.ToList());
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