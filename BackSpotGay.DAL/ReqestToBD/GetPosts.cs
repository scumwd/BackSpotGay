using System;
using System.Collections.Generic;
using BackSpotGay.DAL.Models;
using Npgsql;

namespace BackSpotGay.DAL.ReqestToBD
{
    public class GetPosts
    {
        public static List<Post> Get()
        {
            var postList = new List<Post>();
            using var con = new NpgsqlConnection(Connection.Connetionstring);
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто.");
                string sql = "select * from posts";
                using var cmd = new NpgsqlCommand(sql, con);
                using var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    var post = new Post();
                    post.TextPost = (string) read.GetValue(0);
                    post.TextPath = (string) read.GetValue(1);
                    postList.Add(post);
                }
                return postList;
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