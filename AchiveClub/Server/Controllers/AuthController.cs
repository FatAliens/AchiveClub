using Microsoft.AspNetCore.Mvc;
using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AchiveClub.Server.Mappers;

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
        public ActionResult<UserInfo> Login(LoginParams loginParams)
        {
            User user;
            try
            {
                user = _dbContext.Users
                    .Where(u => u.Email == loginParams.Email && u.Password == loginParams.Password)
                    .Include(u => u.Club)
                    .Include(u => u.CompletedAchivements)
                    .First();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return UserToUserInfoMapper.Map(user, _dbContext.Achivements.ToList());
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
                        Password = registerParams.Password,
                        ClubRefId = 1
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
