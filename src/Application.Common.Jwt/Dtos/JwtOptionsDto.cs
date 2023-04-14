namespace Application.Common.Jwt.Dtos
{
    public class JwtOptionsDto
    {
        public bool Enabled { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretId { get; set; }
        public int Expiration { get; set; }
        public string ProtectorId { get; set; }
        public string ApplicationId { get; set; }
    }
}