using BackSpotGay.DAL.Models;
using Npgsql.Internal.TypeMapping;

namespace BAC.DbHelper
{
    public class CreateEntityUsers
    {
        public static User CreateEntity(string login, string password)
        {
            var user = new User();
            user.Login = login;
            user.Password = password;
            return user;
        }
    }
}