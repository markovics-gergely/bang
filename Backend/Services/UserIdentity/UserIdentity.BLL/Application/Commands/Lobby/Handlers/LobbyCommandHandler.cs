using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;

using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class LobbyCommandHandler : 
        IRequestHandler<CreateLobbyAccountCommand, Unit>,
        IRequestHandler<DeleteLobbyAccountCommand, Unit>,
        IRequestHandler<CreateLobbyCommand, string>,
        IRequestHandler<DeleteLobbyCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILobbyStore _lobbyStore;
        private readonly IAccountStore _accountStore;

        public LobbyCommandHandler(IMapper mapper, ILobbyStore lobbyStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _lobbyStore = lobbyStore;
            _accountStore = accountStore;
        }

        public async Task<Unit> Handle(CreateLobbyAccountCommand request, CancellationToken cancellationToken)
        {
            await _lobbyStore.CreateLobbyAccountAsync(_accountStore.GetActualAccountId(), request.Password, cancellationToken);

            return Unit.Value;
        }
        public async Task<Unit> Handle(DeleteLobbyAccountCommand request, CancellationToken cancellationToken)
        {
            await _lobbyStore.DeleteLobbyAccountAsync(_accountStore.GetActualAccountId(), cancellationToken);

            return Unit.Value;
        }

        public async Task<string> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            return await _lobbyStore.CreateLobbyAsync(_accountStore.GetActualAccountId(), cancellationToken);
        }

        public async Task<Unit> Handle(DeleteLobbyCommand request, CancellationToken cancellationToken)
        {
            await _lobbyStore.DeleteLobbyAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
