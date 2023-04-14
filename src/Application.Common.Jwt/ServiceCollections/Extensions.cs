using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Jwt.Configurations;
using Application.Common.Jwt.Dtos;
using System.Text;

namespace Application.Common.Jwt.ServiceCollections
{
    public static class Extensions
    {
        /// <summary>
        /// The JWT section name
        /// </summary>
        private static readonly string JwtSectionName = "Jwt";

        public static IServiceCollection AddJwtServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<JwtOptionsDto>(JwtSectionName);
            services.Configure<JwtOptionsDto>(configuration.GetSection(JwtSectionName));

            if (options.Enabled)
            {
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SecretId));
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ClockSkew = TimeSpan.Zero,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = options.Issuer,
                            ValidAudience = options.Audience,
                            IssuerSigningKey = signingKey
                        };
                    });
            }
            return services;
        }
    }
}