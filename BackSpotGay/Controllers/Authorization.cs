using System;
using System.Threading.Tasks;
using BAC.DataBase;
using BackSpotGay.Interfaces;
using BackSpotGay.Request;
using Microsoft.AspNetCore.Mvc;

namespace BackSpotGay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthorizationController : Controller
    {
        private readonly IUserService userService;
 
        public AuthorizationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromForm] string login, [FromForm] string password,
            [FromForm] string repeatpassword)
        {
            if (DBConnect.Create(login, password, repeatpassword))
                return Ok();
            return BadRequest("Invalid credentials");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm]string user,[FromForm] string password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Username = user;
            loginRequest.Password = password;
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Missing login details");
            }
 
            var loginResponse = await userService.Login(loginRequest);
 
            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }
            return Ok(loginResponse);
        }
    }
}