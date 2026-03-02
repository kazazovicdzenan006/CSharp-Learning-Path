using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.DTOs.IdentityDto;
using Services.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace API_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _auth.Register(dto);
            return Ok("Registration Successfull");
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var userDto = await _auth.Login(dto);
            return Ok(userDto);
        }

        [HttpPost("AddAdmin/{email}")]
        public async Task<ActionResult> AddAdmin(string email)
        {
            await _auth.AssignAdminRole(email);
            return Ok();
        }

        [HttpGet("Me")]
        [Authorize]
        public ActionResult<object> Me()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            return Ok(new { Name = User.Identity?.Name, IsAuthenticated = User.Identity?.IsAuthenticated, Roles = roles, Claims = claims });
        }


        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            return Ok(new
            {
                Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value),
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
