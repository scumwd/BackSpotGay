using System;
using Npgsql;

namespace BackSpotGay.DAL.ReqestToBD
{
    public class ChangePassword
    {
        public static void Change(string login, string password)
        {
            using var con = new NpgsqlConnection(Connection.Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                string sql = "update users set password = @password where login = @login";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@login",login);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
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