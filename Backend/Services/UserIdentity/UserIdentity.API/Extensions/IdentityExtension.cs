using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserIdentity.API.Extensions
{
    public static class IdentityExtension
    {
        public static void AddIdentityExtensions(this IServiceCollection services, IConfiguration configuration)
        {        
            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<UserIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
                options.IssuerUri = configuration.GetValue<string>("Authentication:AuthorityDocker");
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(GetIdentityResources(configuration))
                .AddInMemoryApiScopes(GetApiScopes(configuration))
                .AddInMemoryClients(GetClients(configuration))
                .AddAspNetIdentity<Account>();
        }

        private static IEnumerable<IdentityResource> GetIdentityResources(IConfiguration configuration)
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        private static IEnumerable<ApiScope> GetApiScopes(IConfiguration configuration)
        {
            return new List<ApiScope>
            {
                new ApiScope(configuration.GetValue<string>("IdentityServer:Name"), configuration.GetValue<string>("IdentityServer:Description"), 
                    new string[] {
                        ClaimTypes.NameIdentifier,
                        JwtClaimTypes.Name,
                        ClaimTypes.Role
                    }
                )
            };
        }
        private static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = configuration.GetValue<string>("IdentityServer:ClientId"),
                    ClientSecrets = { new Secret(configuration.GetValue<string>("IdentityServer:ClientSecret").Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { configuration.GetValue<string>("IdentityServer:Name"), IdentityServerConstants.StandardScopes.OfflineAccess },
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AlwaysSendClientClaims = true
                }
            };
        }
    } 
}
