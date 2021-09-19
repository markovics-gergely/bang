using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL.Domain;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class AccountCommandHandler :
            IRequestHandler<CreateAccountCommand, bool>,
            IRequestHandler<DeleteAccountCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAccountStore _accountStore;

        public AccountCommandHandler(IMapper mapper, IAccountStore accountStore)
        {
            _mapper = mapper;
            _accountStore = accountStore;
        }

        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {          
            if (request.Dto.Password != request.Dto.ConfirmedPassword)
            {
                throw new InvalidParameterException("A megadott jelszó nem egyezik!");
            }

            var account = new Account() { UserName = request.Dto.UserName.Trim() };

            return await _accountStore.CreateAccountAsync(account, request.Dto.Password, cancellationToken);
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account() { Id = _accountStore.GetActualAccountId() };

            await _accountStore.DeleteAccountAsync(account, cancellationToken);

            return Unit.Value;
        }
    }
}
