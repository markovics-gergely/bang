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

        public FriendQueryHandler(IMapper mapper, IFriendStore friendStore)
        {
            _mapper = mapper;
            _friendStore = friendStore;
        }

        public async Task<IEnumerable<FriendViewModel>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _friendStore.GetFriendsAsync(cancellationToken);

            List<Friend> unacceptedFriends = new List<Friend>();
            foreach (var unacceptedFriend in domain)
            {
                if (domain.Contains(unacceptedFriend))
                    unacceptedFriends.Add(unacceptedFriend);
            }

            var list = _mapper.Map<IEnumerable<FriendViewModel>>(unacceptedFriends);

            return list.Select(friend => { friend.IsAccepted = true; return friend; });
        }

        public async Task<IEnumerable<FriendViewModel>> Handle(GetUnacceptedFriendsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _friendStore.GetFriendsAsync(cancellationToken);

            List<Friend> unacceptedFriends = new List<Friend>();
            foreach (var unacceptedFriend in domain)
            {
                if (!domain.Contains(unacceptedFriend))
                    unacceptedFriends.Add(unacceptedFriend);
            }

            var list = _mapper.Map<IEnumerable<FriendViewModel>>(unacceptedFriends);

            return list.Select(friend => { friend.IsAccepted = false; return friend; });
        }
    }
}
