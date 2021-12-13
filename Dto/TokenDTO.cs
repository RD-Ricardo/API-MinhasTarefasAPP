using System;

namespace MyTaskApp_Api.Dto
{
    public class TokenDTO
    {
        public string  Token { get; set; }
        public DateTime Expiration { get; set; }
        public string  RefreshToken { get; set; }
        public DateTime ExpirationRefreshtoken { get; set; }
    }
}