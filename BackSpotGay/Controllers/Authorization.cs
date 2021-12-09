using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BAC.DataBase;
using BackSpotGay.DAL.ReqestToBD;
using BackSpotGay.Interfaces;
using BackSpotGay.Models;
using BackSpotGay.Request;
using Microsoft.AspNetCore.Mvc;

namespace BackSpotGay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthorizationController : Controller
    {
        private readonly IUserService userService;
        private JwtSecurityToken _token;
        public User CurrentUser { get => GetUser(); }
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
                return RedirectToAction("Authorization","Home");
            return BadRequest("Invalid credentials");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm]string user, [FromForm]string password)
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
            Response.Cookies.Append("token",loginResponse.Token);
            return RedirectToAction("UserInfo","Home");
        }

        [Route("change")]
        public async Task<IActionResult> ChangePassword([FromForm] string password, [FromForm]string repeatpassword)
        {
            if (DBConnect.ChangePassword(CurrentUser.Login, password, repeatpassword))
                return RedirectToAction("Index", "Home");
            return BadRequest("Passwords don't match");
        }
        public User GetUser()
        {
            var currentUser = HttpContext.User;

            if (Request.Cookies["token"] == null || Request.Cookies["token"] == "")
            {
                return null;
            }

            var stream = Request.Cookies["token"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            _token = jsonToken as JwtSecurityToken;

            var CurrentId = _token.Claims.First(claim => claim.Type == "nameid" ).Value;
            var userfromBd = new DAL.Models.User();
            userfromBd = ResponseFromBd.GetUserDb(CurrentId);
            var user = new User() ;
            user.Login = userfromBd.Login;
            user.Password = userfromBd.Password;
            return user;
        }
        
    }
}