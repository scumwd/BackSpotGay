using System;
using BackSpotGay.DAL.Models;
using Npgsql;

namespace BackSpotGay.DAL.ReqestToBD
{
    public class CreateUser
    {
        public static void Create(string login, string password, Guid id)
        {
            using var con = new NpgsqlConnection(Connection.Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                string sql = "insert into users (id,login,password) values (@id,@login,@password)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
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