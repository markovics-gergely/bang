using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration.GetValue<string>("Authentication:AuthorityDocker");
                    options.Audience = configuration.GetValue<string>("Authentication:AuthorityDocker") + "/resources";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {

                    builder.SetIsOriginAllowed((host) => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
    }
}
