using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bang.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration.GetValue<string>("Authentication:AuthorityDocker");
                    options.Audience = configuration.GetValue<string>("Authentication:AuthorityDocker") + "/resources";
                    options.RequireHttpsMetadata = false;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
    }
}
