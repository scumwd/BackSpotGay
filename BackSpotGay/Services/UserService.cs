using System.Threading.Tasks;
using BackSpotGay.DAL;
using BackSpotGay.Request;
using BackSpotGay.Responses;
using BackSpotGay.DAL.Models;
using BackSpotGay.Helpers;
using BackSpotGay.Interfaces;

namespace BackSpotGay.Services
{
    public class UserService : IUserService
    {
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = new User();
            user.Login = loginRequest.Username;
            user.Password = loginRequest.Password;
            string token = "";
            if (!Chek.ChekExistUser(loginRequest.Username, loginRequest.Password))
            {
                return null;
            }
            token = await Task.Run( () => TokenHelper.GenerateToken(user));
            return new LoginResponse { Username = user.Login, Token = token };
        }
    }
}