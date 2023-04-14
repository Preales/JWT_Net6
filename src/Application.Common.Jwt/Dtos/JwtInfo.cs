namespace Application.Common.Jwt.Dtos
{
    public class JwtInfo
    {
        public string User { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}