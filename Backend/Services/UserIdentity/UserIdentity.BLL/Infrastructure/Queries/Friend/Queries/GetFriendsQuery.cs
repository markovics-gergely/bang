using UserIdentity.BLL.Infrastructure.Queries.Friend.ViewModels;

using MediatR;

namespace UserIdentity.BLL.Infrastructure.Queries.Friend.Queries
{
    public class GetFriendsQuery : IRequest<FriendViewModel>  
    {
    }
}
