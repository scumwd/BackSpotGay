using BAC.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace BackSpotGay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthorizationController : Controller
    {
        [HttpPost]
        public string Registration([FromForm] string login, [FromForm] string password, [FromForm] string repeatpassword) =>
            DBConnect.Create(login, password, repeatpassword);

        [HttpGet]
        public string Login([FromForm] string user, [FromForm] string password) => DBConnect.ChekExist(user, password);
    }
}