using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSpotGay.DAL.ReqestToBD;
using BackSpotGay.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Npgsql;

namespace BackSpotGay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public User CurrentUser { get => GetUser(); }
        public List<Post> GetPost {get=>GetPosts();}
        private JwtSecurityToken _token;
        IWebHostEnvironment _appEnvironment;
        public const string Connetionstring = "Host=localhost;Port=5432;Database=UsersSpotGay";

        public static List<Post> GetPosts()
        {
            var postList = new List<Post>();
            using var con = new NpgsqlConnection(Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                string sql = "select * from posts";
                using var cmd = new NpgsqlCommand(sql, con);
                using var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    var post = new Post();
                    post.TextPost = (string) read.GetValue(0);
                    post.TextPath = (string) read.GetValue(1);
                    postList.Add(post);
                }
                return postList;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                
                Console.WriteLine("Подключение закрыто.");
                con.Close();
            }
        }

        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        
        [Route("Index")]
        public IActionResult Index()
        {
            return View(GetPost);
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

        [Route("CreatePost")]
        public IActionResult CreatePost()
        {
            if (CurrentUser == null)
            {
                return RedirectToAction("Login", "Authorization");
            }

            return View();
        }
        
        
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromForm] string textpost,IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                
                DAL.Models.Post post = new DAL.Models.Post() {TextPost = textpost, TextPath = path};
                DAL.ReqestToBD.AddAvatar.CreatePost(post);
                return RedirectToAction("Index");
            }
            
            return BadRequest("Missing details");
        }
        
        [HttpPost]
        [Route("AddAvatar")]
        public async Task<IActionResult> AddAvatar(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                DAL.Models.User user = new DAL.Models.User() { Login = CurrentUser.Login, PathAvatar = path};
                DAL.ReqestToBD.AddAvatar.ChangeAvatar(user);
                return RedirectToAction("UserInfo");
            }
            
            return BadRequest("Missing details");
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