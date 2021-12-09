using System;

namespace BackSpotGay.Responses
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}