using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Application.MappingProfiles;
using UserIdentity.BLL.Infrastructure.Stores;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

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

using IdentityServer4.Configuration;
using MediatR;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.BLL.Infrastructure.Queries.Handlers;
using System.Collections.Generic;
using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Commands.User.Handlers;
using System;

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

            services.AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions()
                {
                    LogoutUrl = "/Account/Logout",
                    LoginUrl = "/Account/Login",

                    LoginReturnUrlParameter = "returnUrl"
                };
                options.Authentication.CookieAuthenticationScheme = IdentityConstants.ApplicationScheme;
            })
            .AddAspNetIdentity<Account>();

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserIdentity.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
