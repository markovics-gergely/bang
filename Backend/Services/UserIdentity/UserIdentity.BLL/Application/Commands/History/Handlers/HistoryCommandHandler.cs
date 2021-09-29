using System;
using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;

using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class HistoryCommandHandler :
        IRequestHandler<CreateHistoryCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IHistoryStore _historyStore;
        private readonly IAccountStore _accountStore;

        public HistoryCommandHandler(IMapper mapper, IHistoryStore historyStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _historyStore = historyStore;
            _accountStore = accountStore;
        }

        public async Task<Unit> Handle(CreateHistoryCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            await _historyStore.CreateHistoryAsync(ownId, request.PlayedRole, request.Placement, cancellationToken);

            return Unit.Value;              
        }
    }
}
