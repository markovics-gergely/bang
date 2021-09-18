using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Commands.Handlers;
using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Application.MappingProfiles;
using UserIdentity.BLL.Infrastructure.Queries.Handlers;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Stores;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Hellang.Middleware.ProblemDetails;
using MediatR;

using System.Linq;
using NSwag;
using NSwag.Generation.Processors.Security;
using NSwag.AspNetCore;
using IdentityServer4.Configuration;

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
            services.AddScoped<ILobbyStore, LobbyStore>();

            services.AddScoped<IRequestHandler<CreateAccountCommand, bool>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<LoginAccountCommand, Unit>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteAccountCommand, Unit>, AccountCommandHandler>();

            services.AddScoped<IRequestHandler<GetFriendsQuery, IEnumerable<FriendViewModel>>, FriendQueryHandler>();
            services.AddScoped<IRequestHandler<GetUnacceptedFriendsQuery, IEnumerable<FriendViewModel>>, FriendQueryHandler>();
            services.AddScoped<IRequestHandler<CreateFriendCommand, Unit>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteFriendCommand, Unit>, FriendCommandHandler>();

            services.AddScoped<IRequestHandler<GetLobbyAccountsQuery, IEnumerable<LobbyAccountViewModel>>, LobbyQueryHandler>();
            services.AddScoped<IRequestHandler<GetLobbyAccountsQuery, IEnumerable<LobbyAccountViewModel>>, LobbyQueryHandler>();
            services.AddScoped<IRequestHandler<CreateLobbyAccountCommand, Unit>, LobbyCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteLobbyAccountCommand, Unit>, LobbyCommandHandler>();
            services.AddScoped<IRequestHandler<CreateLobbyCommand, string>, LobbyCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteLobbyCommand, Unit>, LobbyCommandHandler>();

            services.AddMvc();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddControllers();

            services.AddAutoMapper(typeof(AccountProfile));
            services.AddAutoMapper(typeof(FriendProfile));
            services.AddAutoMapper(typeof(LobbyProfile));

            services.AddHttpContextAccessor();

            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<UserIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions()
                {
                    LoginUrl = "/Account/signin",
                    LogoutUrl = "/Account/signout",

                    LoginReturnUrlParameter = "returnUrl"
                };
                options.Authentication.CookieAuthenticationScheme = IdentityConstants.ApplicationScheme;
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryPersistedGrants()
            .AddInMemoryIdentityResources(Configuration.GetSection("IdentityServer:IdentityResources"))
            .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
            .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
            .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
            .AddAspNetIdentity<Account>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("api-openid", policy => policy.RequireAuthenticatedUser()
                    .RequireClaim("scope", "api-openid")
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.DefaultPolicy = options.GetPolicy("api-openid");
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetValue<string>("Authentication:Authority");
                options.Audience = Configuration.GetValue<string>("Authentication:Audience");
                options.RequireHttpsMetadata = false;
            });

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

            services.AddOpenApiDocument(document =>
            {
                document.Title = "User Identity";
                document.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Flows = new OpenApiOAuthFlows()
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = $"{Configuration.GetValue<string>("Authentication:Authority")}/connect/authorize",
                            TokenUrl = $"{Configuration.GetValue<string>("Authentication:Authority")}/connect/token",
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "OpenId" },
                                { "api-openid", "all" }
                            }
                        }
                    }
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Cors");

            app.UseOpenApi();
            app.UseSwaggerUi3(config =>
            {
                config.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "useridentity-swagger",
                    ClientSecret = null,
                    UsePkceWithAuthorizationCodeGrant = true,
                    ScopeSeparator = " ",
                    Realm = null,
                    AppName = "User Identity"
                };
            });

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
