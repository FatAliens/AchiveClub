using Microsoft.AspNetCore.Mvc;
using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private ApplicationContext _dbContext;

        public AuthController(ILogger<AuthController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("login", Name = "Login")]
        public ActionResult<CurrentUserInfo> Login(LoginParams loginParams)
        {
            User user;
            try
            {
                user = _dbContext.Users.Where(u => u.Email == loginParams.Email && u.Password == loginParams.Password).Include(u => u.CompletedAchivements).First();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return UserToUserInfo(user);
        }

        public CurrentUserInfo UserToUserInfo(User user)
        {
            var achivementsInfo = new List<AchiveInfo>();

            foreach (var achive in _dbContext.Achivements.ToList())
            {
                achivementsInfo.Add(new AchiveInfo()
                {
                    Id = achive.Id,
                    Title = achive.Title,
                    Description = achive.Description,
                    Xp = achive.Xp,
                    LogoURL = achive.LogoURL,
                    Completed = user.CompletedAchivements.Where(a => a.AchiveRefId == achive.Id).Any()
                });
            }

            var userInfo = new CurrentUserInfo()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Achivements = achivementsInfo
            };

            return userInfo;
        }

        [HttpPost("register", Name = "Register")]
        public ActionResult Register(RegisterParams registerParams)
        {
            try
            {
                _dbContext.Users.Add(
                    new User()
                    {
                        FirstName = registerParams.FirstName,
                        LastName = registerParams.LastName,
                        Email = registerParams.Email,
                        Password = registerParams.Password
                    });
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
