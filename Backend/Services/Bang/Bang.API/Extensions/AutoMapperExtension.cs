using Bang.BLL.Application.MappingProfiles;

using Microsoft.Extensions.DependencyInjection;

namespace Bang.API.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CharacterProfile));
            services.AddAutoMapper(typeof(RoleProfile));
            services.AddAutoMapper(typeof(CardProfile));
            services.AddAutoMapper(typeof(PlayerProfile));
            services.AddAutoMapper(typeof(PlayerCardProfile));
            services.AddAutoMapper(typeof(GameBoardProfile));
            services.AddAutoMapper(typeof(GameBoardCardProfile));
        }
    }
}
