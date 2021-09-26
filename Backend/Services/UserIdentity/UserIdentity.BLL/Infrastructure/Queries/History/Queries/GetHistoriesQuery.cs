using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace UserIdentity.BLL.Infrastructure.Queries.Queries
{
    public class GetHistoriesQuery : IRequest<IEnumerable<HistoryViewModel>>
    {
    }
}
