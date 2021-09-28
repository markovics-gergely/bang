using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Commands.Handlers;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Handlers;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Stores;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using MediatR;
using UserIdentity.BLL.Infrastructure.Queries.Handlers;

namespace UserIdentity.API.Extentions
{
    public static class ServiceExtension
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IAccountStore, AccountStore>();
            services.AddScoped<IFriendStore, FriendStore>();
            services.AddScoped<ILobbyStore, LobbyStore>();

            services.AddScoped<IRequestHandler<GetActualAccountIdQuery, string>, AccountQueryHandler>();
            services.AddScoped<IRequestHandler<CreateAccountCommand, bool>, AccountCommandHandler>();
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
            services.AddScoped<IRequestHandler<DeleteLobbyAccountByOwnerCommand, Unit>, LobbyCommandHandler>();
        }
    }
}
