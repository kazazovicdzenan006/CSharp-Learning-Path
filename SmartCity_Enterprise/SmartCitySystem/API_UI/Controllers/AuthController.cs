using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.DTOs.IdentityDto;
using Services.Services;

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

    }
}
