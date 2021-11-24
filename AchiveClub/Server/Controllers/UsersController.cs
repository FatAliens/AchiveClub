using System;
using AchiveClub.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AchiveClub.Shared.DTO;

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
            var users = new List<UserInfo>();
            users.Add(new UserInfo()
            {
                FirstName = "Hello World",
                Id = 1,
                Achivements = new List<AchiveInfo>()
            });

            return users;
        }

        private List<UserInfo> UsersToUserInfo(List<User> users)
        {
            throw new NotImplementedException();
        }

        private UserInfo UserToUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        private List<AchiveInfo> AchivementsToAchiveInfo()
        {
            throw new NotImplementedException();
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
