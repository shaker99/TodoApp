using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.Application.Interfaces;
using __TodoApp.Domain.Entities;
using TodoApp.Application.DTO;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IDataHelper<User> _userHelper;
        private readonly IConfiguration _config;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IDataHelper<User> userHelper, IConfiguration config, ILogger<AccountController> logger)
        {
            _userHelper = userHelper;
            _config = config;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation("Login attempt for user: {Username}", loginDto.Username);

            var users = await _userHelper.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

            if (user == null)
            {
                _logger.LogWarning("Login failed for user: {Username}", loginDto.Username);
                return Unauthorized("Username or password not correct");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            _logger.LogInformation("Login successful for user: {Username}", loginDto.Username);

            return Ok(new { token = tokenString });
        }
    }
}
