using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BackSpotGay.DAL.ReqestToBD;
using BackSpotGay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BackSpotGay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public User CurrentUser { get => GetUser(); }
        private JwtSecurityToken _token;
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Authorization")]
        public IActionResult Authorization()
        {
            if(CurrentUser==null)
                return View();
            return RedirectToAction("UserInfo");
        }

        [Route("Registration")]
        public IActionResult Registration()
        {
            if(CurrentUser==null)
                return View();
            return RedirectToAction("UserInfo");
        }

        [Route("UserInfo")]
        public IActionResult UserInfo()
        {

            if (CurrentUser == null)
            {
                return RedirectToAction("Login", "Authorization");
            }

            return View(CurrentUser);
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