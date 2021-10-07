using Bang.BLL.Application.Commands.Commands;
using Bang.BLL.Application.Commands.Handlers;
using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.Handlers;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Stores;

using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bang.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            services.AddHttpClient<IAccountStore, AccountStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IAccountStore, AccountStore>();
            services.AddTransient<ICharacterStore, CharacterStore>();
            services.AddTransient<IRoleStore, RoleStore>();
            services.AddTransient<ICardStore, CardStore>();
            services.AddTransient<IPlayerStore, PlayerStore>();
            services.AddTransient<IGameBoardStore, GameBoardStore>();

            services.AddTransient<IRequestHandler<GetCharacterByTypeQuery, CharacterViewModel>, CharacterQueryHandler>();
            services.AddTransient<IRequestHandler<GetCharactersQuery, IEnumerable<CharacterViewModel>>, CharacterQueryHandler>();
            services.AddTransient<IRequestHandler<GetRoleByTypeQuery, RoleViewModel>, RoleQueryHandler>();
            services.AddTransient<IRequestHandler<GetRolesQuery, IEnumerable<RoleViewModel>>, RoleQueryHandler>();
            services.AddTransient<IRequestHandler<GetCardByTypeQuery, CardViewModel>, CardQueryHandler>();
            services.AddTransient<IRequestHandler<GetCardsQuery, IEnumerable<CardViewModel>>, CardQueryHandler>();

            services.AddTransient<IRequestHandler<GetPlayerQuery, PlayerViewModel>, PlayerQueryHandler>();
            services.AddTransient<IRequestHandler<GetPlayersByGameBoardQuery, IEnumerable<PlayerViewModel>>, PlayerQueryHandler>();
            services.AddTransient<IRequestHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>, PlayerQueryHandler>();
            services.AddTransient<IRequestHandler<GetTargetablePlayersQuery, IEnumerable<PlayerViewModel>>, PlayerQueryHandler>();
            services.AddTransient<IRequestHandler<DecrementPlayerHealthCommand, Unit>, PlayerCommandHandler>();
            services.AddTransient<IRequestHandler<PlayCardCommand, Unit>, CardCommandHandler>();

            services.AddTransient<IRequestHandler<GetGameBoardQuery, GameBoardViewModel>, GameBoardQueryHandler>();
            services.AddTransient<IRequestHandler<GetGameBoardByUserQuery, GameBoardByUserViewModel>, GameBoardQueryHandler>();
            services.AddTransient<IRequestHandler<GetGameBoardsQuery, IEnumerable<GameBoardViewModel>>, GameBoardQueryHandler>();
            services.AddTransient<IRequestHandler<GetGameBoardCardsOnTopQuery, IEnumerable<FrenchCardViewModel>>, GameBoardQueryHandler>();
            services.AddTransient<IRequestHandler<GetLastDiscardedGameBoardCardQuery, FrenchCardViewModel>, GameBoardQueryHandler>();
            services.AddTransient<IRequestHandler<CreateGameBoardCommand, long>, GameBoardCommandHandler>();
            services.AddTransient<IRequestHandler<DiscardFromDrawableGameBoardCardCommand, FrenchCardViewModel>, GameBoardCommandHandler>();
        }
    }
}
