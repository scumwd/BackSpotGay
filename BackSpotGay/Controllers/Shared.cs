using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using BackSpotGay.DAL.ReqestToBD;
using BackSpotGay.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackSpotGay.Controllers
{
    public class Shared : Controller
    {
        public User CurrentUser { get => GetUser(); }
        private JwtSecurityToken _token;
        public IActionResult _Layout()
        {
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
            user.PathAvatar = userfromBd.PathAvatar;
            return user;
        }
    }
}