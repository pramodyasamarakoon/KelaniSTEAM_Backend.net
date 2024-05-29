using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KelaniSTEAM_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService) =>
        _authService = authService;

    // Route: POST api/user/register
    [HttpPost]
    [Route("register")]
    public IActionResult Register(RegisterDto registerDto)
    {
        var userExists = _authService.GetUserByUsername(registerDto.UserName);
        if (userExists != null)
            return BadRequest(new { message = "Username already exists" });

        var user = new User
        {
            UserName = registerDto.UserName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        _authService.CreateUser(user);

        return Ok(new { message = "User registered successfully" });
    }

    // Route: POST api/user/login
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginDto loginDto)
    {
        var user = _authService.Authenticate(loginDto.UserName, loginDto.Password);

        if (user == null)
            return BadRequest(new { message = "Invalid username or password" });

        // Generate JWT token (you can use any JWT library for this)
        var token = GenerateJwtToken(user);

        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        byte[] key = new byte[32];
        new Random().NextBytes(key);
        return Convert.ToBase64String(key);
    }
}
