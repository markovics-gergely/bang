using Bang.BLL.Application.Commands.Commands;
using Bang.BLL.Application.Commands.Handlers;
using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.BLL.Application.MappingProfiles;
using Bang.BLL.Infrastructure.Queries.Handlers;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Stores;
using Bang.DAL;

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Hangfire;
using Hangfire.MemoryStorage;
using Hellang.Middleware.ProblemDetails;
using MediatR;

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
            services.AddDbContext<BangDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICharacterStore, CharacterStore>();
            services.AddScoped<IRoleStore, RoleStore>();
            services.AddScoped<ICardStore, CardStore>();
            services.AddScoped<IPlayerStore, PlayerStore>();
            services.AddScoped<IGameBoardStore, GameBoardStore>();

            services.AddScoped<IRequestHandler<GetCharacterByTypeQuery, CharacterViewModel>, CharacterQueryHandler>();
            services.AddScoped<IRequestHandler<GetCharactersQuery, IEnumerable<CharacterViewModel>>, CharacterQueryHandler>();
            services.AddScoped<IRequestHandler<GetRoleByTypeQuery, RoleViewModel>, RoleQueryHandler>();
            services.AddScoped<IRequestHandler<GetRolesQuery, IEnumerable<RoleViewModel>>, RoleQueryHandler>();
            services.AddScoped<IRequestHandler<GetCardByTypeQuery, CardViewModel>, CardQueryHandler>();
            services.AddScoped<IRequestHandler<GetCardsQuery, IEnumerable<CardViewModel>>, CardQueryHandler>();

            services.AddScoped<IRequestHandler<GetPlayerQuery, PlayerViewModel>, PlayerQueryHandler>();
            services.AddScoped<IRequestHandler<GetPlayersByGameBoardQuery, IEnumerable<PlayerViewModel>>, PlayerQueryHandler>();
            services.AddScoped<IRequestHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>, PlayerQueryHandler>();

            services.AddScoped<IRequestHandler<GetGameBoardQuery, GameBoardViewModel>, GameBoardQueryHandler>();
            services.AddScoped<IRequestHandler<GetGameBoardsQuery, IEnumerable<GameBoardViewModel>>, GameBoardQueryHandler>();
            services.AddScoped<IRequestHandler<CreateGameBoardCommand, long>, GameBoardCommandHandler>();

            services.AddAutoMapper(typeof(CharacterProfile));
            services.AddAutoMapper(typeof(RoleProfile));
            services.AddAutoMapper(typeof(CardProfile));
            services.AddAutoMapper(typeof(PlayerProfile));
            services.AddAutoMapper(typeof(PlayerCardProfile));
            services.AddAutoMapper(typeof(GameBoardProfile));
            services.AddAutoMapper(typeof(GameBoardCardProfile));

            services.AddMvc();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseDefaultTypeSerializer()
                .UseMemoryStorage()
            );
            services.AddHangfireServer();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School.API", Version = "v1" });
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
                }
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager)
        {
            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "School.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Cors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            recurringJobManager.AddOrUpdate(
                "",
                () => Debug.WriteLine(""),
                Cron.Hourly
            );
        }
    }
}
