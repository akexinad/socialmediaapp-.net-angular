using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            username = username.ToLower();

            // Checking if the user already exists.
            if (await _repo.UserExists(username))
                // The method BadRequest() comes from the ControllerBase class.
                return BadRequest("Username already exists");

            // Creating a new user and storing the newly created user name.
            var userToCreate = new User
            {
                Username = username
            };

            // Here we call the Register function from the AuthRepo and creating the password hash and salt.
            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);
        }

    }
}