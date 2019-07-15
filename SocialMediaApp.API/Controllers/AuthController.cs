using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Dtos;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            // If we were not using ApiContoller, it would not be able to read the
            // validations from the Dto and would thus have to add the [FromBody] attribute 
            // before the UserForRegisterDto argument in the Register method as well 
            // as the manual velidator below write them manually as such:

            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            // Checking if the user already exists.
            if (await _repo.UserExists(userForRegisterDto.Username))
                // The method BadRequest() comes from the ControllerBase class.
                return BadRequest("Username already exists");

            // Creating a new user and storing the newly created user name.
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            // Here we call the Register function from the AuthRepo and creating the password hash and salt.
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            // checking if the user exists.
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            // We dont want to give away too many hints to the user regarding
            // incorrect username or password, so we will just provide them with
            // a general unauthorized error.
            if (userFromRepo == null)
                return Unauthorized();

            // This is all that is involved in buildling up the token.

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}