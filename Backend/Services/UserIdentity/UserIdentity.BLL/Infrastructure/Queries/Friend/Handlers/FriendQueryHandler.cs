using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

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
            var ownId = _accountStore.GetActualAccountId();

            var domain = await _friendStore.GetFriendsAsync(ownId, cancellationToken);

            List<Friend> acceptedFriends = new List<Friend>();

            foreach (var acceptedFriend in domain)
            {
                if (domain.Contains(acceptedFriend))
                {
                    acceptedFriends.Add(acceptedFriend);
                    if (acceptedFriends.Contains(acceptedFriend))
                    {
                        acceptedFriends.RemoveAll(f => 
                            (f.ReceiverId == acceptedFriend.SenderId || f.ReceiverId == acceptedFriend.ReceiverId) && 
                            f.SenderId == ownId);
                    }
                }
            }

            return _mapper.Map<IEnumerable<FriendViewModel>>(acceptedFriends);
        }

        public async Task<IEnumerable<FriendViewModel>> Handle(GetUnacceptedFriendsQuery request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            var domain = await _friendStore.GetFriendsAsync(ownId, cancellationToken);

            List<Friend> unacceptedFriends = new List<Friend>();

            foreach (var unacceptedFriend in domain)
            {
                if (!domain.Contains(unacceptedFriend))
                {            
                    if(unacceptedFriend.ReceiverId == ownId)
                    {
                        unacceptedFriends.Add(unacceptedFriend);
                    }
                }
            }

            return _mapper.Map<IEnumerable<FriendViewModel>>(unacceptedFriends);
        }
    }
}
