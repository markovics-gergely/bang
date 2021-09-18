using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using UserIdentity.DAL.Domain;

namespace UserIdentity.BLL.Infrastructure.Queries.Handlers
{
    public class FriendQueryHandler :
        IRequestHandler<GetFriendsQuery, IEnumerable<FriendViewModel>>,
        IRequestHandler<GetUnacceptedFriendsQuery, IEnumerable<FriendViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IFriendStore _friendStore;
        private readonly IAccountStore _accountStore;

        public FriendQueryHandler(IMapper mapper, IFriendStore friendStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _friendStore = friendStore;
            _accountStore = accountStore;
        }

        public async Task<IEnumerable<FriendViewModel>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _friendStore.GetFriendsAsync(_accountStore.GetActualAccountId(), cancellationToken);

            List<Friend> unacceptedFriends = new List<Friend>();
            foreach (var unacceptedFriend in domain)
            {
                if (domain.Contains(unacceptedFriend))
                    unacceptedFriends.Add(unacceptedFriend);
            }

            return _mapper.Map<IEnumerable<FriendViewModel>>(unacceptedFriends);
        }

        public async Task<IEnumerable<FriendViewModel>> Handle(GetUnacceptedFriendsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _friendStore.GetFriendsAsync(_accountStore.GetActualAccountId(), cancellationToken);

            List<Friend> unacceptedFriends = new List<Friend>();
            foreach (var unacceptedFriend in domain)
            {
                if (!domain.Contains(unacceptedFriend))
                    unacceptedFriends.Add(unacceptedFriend);
            }

            return _mapper.Map<IEnumerable<FriendViewModel>>(unacceptedFriends);
        }
    }
}
