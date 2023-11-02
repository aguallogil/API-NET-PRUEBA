using PruebaAPI.Services;
using DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace PruebaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUsuarioSVC _userService;
        public AccountController(IUsuarioSVC userService) 
        { 
            this._userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario data)
        {
            if (data == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _userService.Login(data);

            if (user.Access_Token == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }
    }
}
