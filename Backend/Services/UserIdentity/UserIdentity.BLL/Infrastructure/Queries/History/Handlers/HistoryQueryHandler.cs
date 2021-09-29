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
    public class HistoryQueryHandler :
        IRequestHandler<GetHistoriesQuery, IEnumerable<HistoryViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IHistoryStore _historyStore;
        private readonly IAccountStore _accountStore;

        public HistoryQueryHandler(IMapper mapper, IHistoryStore historyStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _historyStore = historyStore;
            _accountStore = accountStore;
        }

        public async Task<IEnumerable<HistoryViewModel>> Handle(GetHistoriesQuery request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            var domain = await _historyStore.GetHistoriesAsync(ownId, cancellationToken);

            return _mapper.Map<IEnumerable<HistoryViewModel>>(domain);
        }
    }
}
