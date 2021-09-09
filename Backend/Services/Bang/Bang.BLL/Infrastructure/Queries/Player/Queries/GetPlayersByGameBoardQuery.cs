using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetPlayersByGameBoardQuery : IRequest<IEnumerable<PlayerViewModel>>
    {
        public long gameBoardId { get; set; }

        public GetPlayersByGameBoardQuery(long gid)
        {
            gameBoardId = gid;
        }
    }
}
