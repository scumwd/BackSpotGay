using System;

namespace BackSpotGay.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PathAvatar { get; set; }
    }
}