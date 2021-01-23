using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyFinances.API.Dto;
using MyFinances.API.Interfaces;
using MyFinances.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            _authRepository = repository;
            _config = configuration;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="userForRegister">DTO model sent from registration form</param>
        /// <response code="201">Returns status 201 if the user has registered correctly</response>
        /// <response code="400">Returns status 400 if there is a user with the same email</response>            
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            userForRegister.Email = userForRegister.Email.ToLower();

            if (await _authRepository.UserExist(userForRegister.Email))
            {
                return BadRequest("Użytkownik istnieje!");
            }

            User user = new User(userForRegister.Email);
            await _authRepository.Register(user, userForRegister.Password);

            return StatusCode(201);
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="userForLogin">DTO model sent from login form</param>
        /// <response code="201">Returns status 200 if the user has login correctly</response>
        /// <response code="401">Returns status 401 if the login details are incorrect </response>          
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            User userFromRepo = await _authRepository.Login(userForLogin.Email.ToLower(), userForLogin.Password);

            if (userFromRepo == null) 
                return Unauthorized("Podaj prawidłowe dane!"); 

            // create token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim("user", userFromRepo.Email.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(240),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}
