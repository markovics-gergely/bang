using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;

using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class FriendCommandHandler : 
        IRequestHandler<AddFriendCommand, Unit>,
        IRequestHandler<DeleteFriendCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IFriendStore _friendStore;

        public FriendCommandHandler(IMapper mapper, IFriendStore friendStore)
        {
            _mapper = mapper;
            _friendStore = friendStore;
        }

        public async Task<Unit> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            await _friendStore.AddFriendAsync("Bejelentkezett felhasználó", request.Id, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            await _friendStore.DeleteFriendAsync("Bejelentkezett felhasználó", request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
