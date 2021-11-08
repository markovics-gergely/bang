using UserIdentity.API.Extensions;
using UserIdentity.API.Hubs.Hubs;
using UserIdentity.DAL;

using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace UserIdentity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserIdentityDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
        
            services.AddAutoMapperExtensions();
            services.AddExceptionExtensions();
            services.AddIdentityExtensions(Configuration);
            services.AddAuthenticationExtensions(Configuration);
            services.AddServiceExtensions();
            services.AddSwaggerExtension(Configuration);

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddSignalR();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserIdentityDbContext dbContext)
        {
            dbContext.Database.Migrate();

            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Identity");
                c.OAuthConfigObject.ClientId = Configuration.GetValue<string>("IdentityServer:ClientId");
                c.OAuthConfigObject.ClientSecret = Configuration.GetValue<string>("IdentityServer:ClientSecret");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.Use(async (context, next) => await AuthQueryStringToHeader(context, next));

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub").RequireCors("CorsPolicy");
                endpoints.MapHub<FriendHub>("/friendhub").RequireCors("CorsPolicy");
                //endpoints.MapHub<LobbyHub>("/lobbyhub");
            });
        }

        private async Task AuthQueryStringToHeader(HttpContext context, Func<Task> next)
        {
            var qs = context.Request.QueryString;

            if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]) && qs.HasValue)
            {
                var token = (from pair in qs.Value.TrimStart('?').Split('&')
                             where pair.StartsWith("token=")
                             select pair.Substring(6)).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
            }

            await next?.Invoke();
        }
    }
}
