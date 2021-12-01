using System.Threading.Tasks;
using BAC.DbHelper;
using BackSpotGay.DAL;
using BackSpotGay.DAL.ReqestToBD;

namespace BAC.DataBase
{
    public class DBConnect
    {
        public static bool Create(string login, string password, string repeatpassword)
        {
            if (string.Equals(password,repeatpassword))
            {
                CreateUser.Create(login,password);
                return true;
            }
            return false;
        }
    }
}