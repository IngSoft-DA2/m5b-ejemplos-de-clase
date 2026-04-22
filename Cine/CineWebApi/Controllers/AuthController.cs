using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthController(ISessionService sessionService) : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
    {
        var token = sessionService.Authenticate(request.Username, request.Password);
        if (token is null)
        {
            return Unauthorized(new { message = "Usuario o password invalidos." });
        }

        return Ok(new LoginResponseDto { Token = token });
    }
}
