using System.Threading.Tasks;
using BackSpotGay.Request;
using BackSpotGay.Responses;

namespace BackSpotGay.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}