using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Queries;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Infrastructure.Queries.Handlers
{
    public class AccountQueryHandler :
        IRequestHandler<GetActualAccountIdQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly IAccountStore _accountStore;

        public AccountQueryHandler(IMapper mapper, IAccountStore accountStore)
        {
            _mapper = mapper;
            _accountStore = accountStore;
        }

        public async Task<string> Handle(GetActualAccountIdQuery request, CancellationToken cancellationToken)
        {
            return _accountStore.GetActualAccountId();
        }
    }
}
