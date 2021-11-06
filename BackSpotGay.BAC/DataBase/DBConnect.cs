using BAC.DbHelper;
using BackSpotGay.DAL;
using BackSpotGay.DAL.ReqestToBD;

namespace BAC.DataBase
{
    public class DBConnect
    {
        public static string Create(string login, string password, string repeatpassword)
        {
            if (string.Equals(password,repeatpassword) && (password!=null || password!=""))
            {
                CreateUser.Create(CreateEntityUsers.CreateEntity(login, password));
                return "Пользователь создан.";
            }
            return "Пароли не совпадают.";
        }

        public static string ChekExist(string user, string password)
        {
            if (Chek.ChekExistUser(user, password))
                return "Успешно!";
            else
                return "Неверный логин или пароль.";
        }
    }
}