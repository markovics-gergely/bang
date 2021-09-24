using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace UserIdentity.API.Extentions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Identity", Version = "v1" });
                c.AddSecurityDefinition(configuration.GetValue<string>("Authentication:SecurityScheme"), new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri(configuration.GetValue<string>("Authentication:Authority") + "/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                [configuration.GetValue<string>("IdentityServer:Name")] = 
                                    configuration.GetValue<string>("IdentityServer:Description")
                            }
                        }
                    },
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer"
                });

                c.OperationFilter<AuthorizeSwaggerOperationFilter>(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = configuration.GetValue<string>("Authentication:SecurityScheme"),
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>(){ configuration.GetValue<string>("IdentityServer:Name") }
                    }
                });
            });
        }
    }

    public class AuthorizeSwaggerOperationFilter : IOperationFilter
    {
        private readonly OpenApiSecurityRequirement requirement;

        public AuthorizeSwaggerOperationFilter(OpenApiSecurityRequirement requirement)
        {
            this.requirement = requirement;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.GetCustomAttribute<AuthorizeAttribute>() != null ||
                context.MethodInfo.DeclaringType.GetCustomAttribute<AuthorizeAttribute>() != null)
            {
                operation.Security.Add(requirement);
                operation.Responses.TryAdd("401", new OpenApiResponse() { Description = "Unauthorized" });
            }
        }
    }
}
