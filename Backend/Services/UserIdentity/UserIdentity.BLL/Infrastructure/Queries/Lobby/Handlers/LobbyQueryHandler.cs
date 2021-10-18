﻿using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Infrastructure.Queries.Handlers
{
    public class LobbyQueryHandler :
        IRequestHandler<GetActualLobbyIdQuery, long>,
        IRequestHandler<GetLobbyAccountsQuery, IEnumerable<LobbyAccountViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILobbyStore _lobbyStore;
        private readonly IAccountStore _accountStore;

        public LobbyQueryHandler(IMapper mapper, ILobbyStore lobbyStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _lobbyStore = lobbyStore;
            _accountStore = accountStore;
        }

        public async Task<long> Handle(GetActualLobbyIdQuery request, CancellationToken cancellationToken)
        {
            var accountId = _accountStore.GetActualAccountId();

            return await _lobbyStore.GetActualLobbyIdAsync(accountId, cancellationToken);
        }

        public async Task<IEnumerable<LobbyAccountViewModel>> Handle(GetLobbyAccountsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _lobbyStore.GetLobbyAccountsAsync(request.LobbyId, cancellationToken);

            return _mapper.Map<IEnumerable<LobbyAccountViewModel>>(domain);
        }
    }
}
