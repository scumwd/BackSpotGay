using System;
using BackSpotGay.DAL.ReqestToBD;
using Npgsql;

namespace BackSpotGay.DAL
{
    public class Chek
    {
        public static bool ChekExistUser(string login, string password)
        {
            using var con = new NpgsqlConnection(Connection.Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                string sql = "select * from users where login=@login and password=@password";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@login",login);
                cmd.Parameters.AddWithValue("@password",password);
                cmd.ExecuteNonQuery();
                using var read = cmd.ExecuteReader();
                if (read.HasRows)
                    return true;
                else return false;
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
    }
}