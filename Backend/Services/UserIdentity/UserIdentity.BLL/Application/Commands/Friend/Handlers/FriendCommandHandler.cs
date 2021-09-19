using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;

using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class FriendCommandHandler : 
        IRequestHandler<CreateFriendCommand, Unit>,
        IRequestHandler<DeleteFriendCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IFriendStore _friendStore;
        private readonly IAccountStore _accountStore;

        public FriendCommandHandler(IMapper mapper, IFriendStore friendStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _friendStore = friendStore;
            _accountStore = accountStore;
        }

        public async Task<Unit> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            await _friendStore.CreateFriendAsync(_accountStore.GetActualAccountId(), request.Name, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            await _friendStore.DeleteFriendAsync(_accountStore.GetActualAccountId(), request.Name, cancellationToken);

            return Unit.Value;
        }
    }
}
