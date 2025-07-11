using Microsoft.AspNetCore.Mvc;
using ServiceStatusHub.Auth.DTOs;
using ServiceStatusHub.Auth.Models;
using ServiceStatusHub.Auth.Services;

namespace ServiceStatusHub.Auth.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Exemplo hardcoded (depois substituir por DB)
        if (request.Username == "admin" && request.Password == "admin")
        {
            var user = new User { Username = request.Username, Role = "Admin" };
            var token = _tokenService.GenerateToken(user);
            return Ok(new { access_token = token });
        }

        return Unauthorized("Usuário ou senha inválidos");
    }
}

