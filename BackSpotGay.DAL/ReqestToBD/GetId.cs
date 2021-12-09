using System;
using Npgsql;

namespace BackSpotGay.DAL.ReqestToBD
{
    public class GetId
    {
        public static Guid GetById(string login)
        {
            using var con = new NpgsqlConnection(Connection.Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                string sql = "select * from users where login=@login";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@login",login);
                using var read = cmd.ExecuteReader();
                read.Read();
                var guid = (Guid) read.GetValue(0);
                return guid;
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