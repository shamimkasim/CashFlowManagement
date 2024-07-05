using Microsoft.AspNetCore.Mvc;
using PostingControlService.Application.Interfaces;
using PostingControlService.Api.Models;
using System.Threading.Tasks;

namespace PostingControlService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.Login(model.Username, model.Password);
            if (token == null)
                return Unauthorized();
            return Ok(new TokenModel { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model.Username, model.Password);
            if (!result)
                return BadRequest("User registration failed.");
            return Ok();
        }
    }
}