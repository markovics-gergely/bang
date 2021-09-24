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

namespace UserIdentity.BLL.Infrastructure.Queries.History.Handlers
{
    public class HistoryQueryHandler :
        IRequestHandler<GetHistoriesQuery, IEnumerable<HistoryViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IFriendStore _friendStore;
        private readonly IAccountStore _accountStore;

        public HistoryQueryHandler(IMapper mapper, IFriendStore friendStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _friendStore = friendStore;
            _accountStore = accountStore;
        }

        public Task<IEnumerable<HistoryViewModel>> Handle(GetHistoriesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
