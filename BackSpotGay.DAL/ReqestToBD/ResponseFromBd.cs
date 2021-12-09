using System;
using BackSpotGay.DAL.Models;
using Npgsql;

namespace BackSpotGay.DAL.ReqestToBD
{
    public class ResponseFromBd
    {
        public static User GetUserDb(string id)
        {
            using var con = new NpgsqlConnection(Connection.Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                var guid = new Guid(id);
                string sql = "select * from users where id=@id";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id",guid);
                cmd.ExecuteNonQuery();
                using var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    read.Read();
                    User user = new User(); 
                    user.Login =(string) read.GetValue(1); 
                    user.Password = (string) read.GetValue(2);
                    return user;

                }
                else return null;
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