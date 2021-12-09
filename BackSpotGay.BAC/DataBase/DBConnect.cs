using System;
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
                var id = Guid.NewGuid();
                CreateUser.Create(login,password,id);
                return true;
            }
            return false;
        }

        public static bool ChangePassword(string login, string password, string repeatpassword)
        {
            if (string.Equals(password, repeatpassword))
            {
                BackSpotGay.DAL.ReqestToBD.ChangePassword.Change(login,password);
                return true;
            }

            return false;
        }
    }
}