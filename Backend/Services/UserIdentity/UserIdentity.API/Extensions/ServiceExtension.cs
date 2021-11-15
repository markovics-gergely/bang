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
using System;

namespace UserIdentity.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient("bang", c =>
            {
                c.BaseAddress = new Uri("http://bang.api");
            });

            services.AddTransient<IAccountStore, AccountStore>();
            services.AddTransient<IFriendStore, FriendStore>();
            services.AddTransient<IHistoryStore, HistoryStore>();
            services.AddTransient<ILobbyStore, LobbyStore>();

            services.AddTransient<IRequestHandler<GetActualAccountIdQuery, string>, AccountQueryHandler>();
            services.AddTransient<IRequestHandler<GetActualAccountStatusQuery, StatusViewModel>, AccountQueryHandler>();
            services.AddTransient<IRequestHandler<CreateAccountCommand, bool>, AccountCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteAccountCommand, Unit>, AccountCommandHandler>();

            services.AddTransient<IRequestHandler<GetFriendsQuery, IEnumerable<FriendViewModel>>, FriendQueryHandler>();
            services.AddTransient<IRequestHandler<GetUnacceptedFriendsQuery, IEnumerable<FriendViewModel>>, FriendQueryHandler>();
            services.AddTransient<IRequestHandler<CreateFriendCommand, Unit>, FriendCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteFriendCommand, Unit>, FriendCommandHandler>();

            services.AddTransient<IRequestHandler<GetActualLobbyQuery, LobbyViewModel>, LobbyQueryHandler>();
            services.AddTransient<IRequestHandler<GetLobbyAccountsQuery, IEnumerable<LobbyAccountViewModel>>, LobbyQueryHandler>();
            services.AddTransient<IRequestHandler<CreateLobbyAccountCommand, Unit>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteLobbyAccountCommand, Unit>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<CreateLobbyCommand, long>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteLobbyAccountByOwnerCommand, Unit>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateLobbyInviteFalseCommand, Unit>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateLobbyInviteTrueCommand, Unit>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<CreateGameBoardCommand, Unit>, LobbyCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateLobbyGameBoardIdCommand, Unit>, LobbyCommandHandler>();

            services.AddTransient<IRequestHandler<GetHistoriesQuery, IEnumerable<HistoryViewModel>>, HistoryQueryHandler>();
            services.AddTransient<IRequestHandler<CreateHistoryCommand, Unit>, HistoryCommandHandler>();  
        }
    }
}
