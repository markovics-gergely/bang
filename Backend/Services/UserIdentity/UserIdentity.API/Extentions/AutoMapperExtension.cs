﻿using UserIdentity.BLL.Application.MappingProfiles;

using Microsoft.Extensions.DependencyInjection;

namespace UserIdentity.API.Extentions
{
    internal static class AutoMapperExtension
    {
        public static void AddAutoMapperExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AccountProfile));
            services.AddAutoMapper(typeof(FriendProfile));
            services.AddAutoMapper(typeof(LobbyProfile));
        }
    }
}
