using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL.Domain;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using UserIdentity.BLL.Application.Exceptions;

namespace UserIdentity.BLL.Application.Commands.User.Handlers
{
    public class AccountCommandHandler :
            IRequestHandler<CreateAccountCommand, bool>,
            IRequestHandler<LoginAccountCommand, Unit>,
            IRequestHandler<DeleteAccountCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAccountStore _accountStore;

        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account() { UserName = request.Dto.UserName.Trim() };

            if (request.Dto.Password != request.Dto.ConfirmedPassword)
            {
                throw new InvalidParameterException("A megadott jelszó nem egyezik!");
            }

            return await _accountStore.CreateAccountAsync(account, request.Dto.Password, cancellationToken);
        }

        public Task<Unit> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
