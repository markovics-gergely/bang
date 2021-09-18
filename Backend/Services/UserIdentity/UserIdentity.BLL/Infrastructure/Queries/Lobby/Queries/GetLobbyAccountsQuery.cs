using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace UserIdentity.BLL.Infrastructure.Queries.Queries
{
    public class GetLobbyAccountsQuery : IRequest<IEnumerable<LobbyAccountViewModel>>  
    {
        public long LobbyId { get; set; }

        public GetLobbyAccountsQuery(long lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}
