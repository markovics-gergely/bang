using UserIdentity.API.Extensions;
using UserIdentity.DAL;

using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Hellang.Middleware.ProblemDetails;
using MediatR;

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

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
