using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class ChatCommandHandler :
        IRequestHandler<CreateMessageCommand, Unit>
    {
        public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
