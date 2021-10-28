using Microsoft.AspNetCore.Mvc;
using AchiveClub.Shared.Models;
using Microsoft.Extensions.Logging;

namespace AchiveClub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private LiteDbContext _dbContext;

        public AuthController(ILogger<AuthController> logger, LiteDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("login",Name = "Login")]
        public ActionResult<User> Login(LoginParams loginParams)
        {
            _logger.LogInformation("Put: Login User");
            var user = _dbContext.Db.GetCollection<User>("Users").Include(u => u.CompletedAchivements).FindOne(u => u.Email == loginParams.Email && u.Password == loginParams.Password);
            if(user != null)
            {
                return user;
            }
            else
            {
                return new BadRequestResult();
            }
        }
        [HttpPost("register", Name = "Register")]
        public ActionResult<User> Register(RegisterParams registerParams)
        {
            _logger.LogInformation("Put: Register User");
            int id = _dbContext.Db.GetCollection<User>("Users").Insert(
                new User()
                {
                    FirstName = registerParams.FirstName,
                    LastName = registerParams.LastName,
                    Email = registerParams.Email,
                    Password = registerParams.Password
                });
            User user = _dbContext.Db.GetCollection<User>("Users").Include(u => u.CompletedAchivements).FindById(id);
            if (user != null)
            {
                return user;
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
