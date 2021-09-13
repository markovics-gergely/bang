using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Commands.Handlers;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Application.MappingProfiles;
using UserIdentity.BLL.Infrastructure.Stores;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MediatR;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.BLL.Infrastructure.Queries.Handlers;
using Hellang.Middleware.ProblemDetails;
using UserIdentity.BLL.Application.Exceptions;
using Microsoft.AspNetCore.Http;

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
            services.AddDbContext<UserIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAccountStore, AccountStore>();
            services.AddScoped<IFriendStore, FriendStore>();

            services.AddScoped<IRequestHandler<GetFriendsQuery, IEnumerable<FriendViewModel>>, FriendQueryHandler>();
            services.AddScoped<IRequestHandler<GetUnacceptedFriendsQuery, IEnumerable<FriendViewModel>>, FriendQueryHandler>();
            services.AddScoped<IRequestHandler<CreateAccountCommand, bool>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<LoginAccountCommand, Unit>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteAccountCommand, Unit>, AccountCommandHandler>();

            services.AddMvc();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserIdentity.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(AccountProfile));
            services.AddAutoMapper(typeof(FriendProfile));

            services.AddHttpContextAccessor();

            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<UserIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Configuration.GetSection("IdentityServer:IdentityResources"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddAspNetIdentity<Account>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "useridentity-api";
                    options.RequireHttpsMetadata = false;
                }
                );

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("AllowedOrigins").Get<string[]>())
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => false;
                options.Map<EntityNotFoundException>(
                (ctx, ex) =>
                {
                    var pd = StatusCodeProblemDetails.Create(StatusCodes.Status404NotFound);
                    pd.Title = ex.Message;
                    return pd;
                });
                options.Map<InvalidParameterException>(
                (ctx, ex) =>
                {
                    var pd = StatusCodeProblemDetails.Create(StatusCodes.Status401Unauthorized);
                    pd.Title = ex.Message;
                    return pd;
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "School.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Cors");
  
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
