using Bang.API.SignalR;
using Bang.API.Extensions;
using Bang.DAL;

using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Hangfire;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using System;

namespace Bang.API
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
            services.AddDbContext<BangDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
      
            services.AddAutoMapperExtensions();
            services.AddExceptionExtensions();
            services.AddAuthenticationExtensions(Configuration);
            services.AddServiceExtensions();
            services.AddSwaggerExtension(Configuration);

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddSignalR();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BangDbContext dbContext)
        {
            dbContext.Database.Migrate();

            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bang");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GameHub>("/game").RequireCors("CorsPolicy");
            });
        }
    }
}
