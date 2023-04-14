using Microsoft.IdentityModel.Tokens;
using Application.Common.Jwt.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Jwt.Helpers
{
    public static class JwtTokenHelper
    {
        public static JwtInfo GenerateToken(JwtOptionsDto configuration, string user, string email, IList<string> roles)
        {
            var listClaims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.UniqueName, user),
                new (JwtRegisteredClaimNames.NameId, user),
                new (JwtRegisteredClaimNames.Email, email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (roles.Any())
                roles.ToList().ForEach(role =>
                {
                    listClaims.Add(new Claim(ClaimTypes.Role, role));
                });

            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(configuration.Expiration));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretId));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                configuration.Issuer,
                configuration.Audience,
                listClaims,
                expires: expiration,
                signingCredentials: cred);

            return new JwtInfo
            {
                User = user,
                Email = email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}