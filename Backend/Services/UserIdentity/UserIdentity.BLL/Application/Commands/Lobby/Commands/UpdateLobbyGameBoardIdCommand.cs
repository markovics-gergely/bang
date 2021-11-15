using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class UpdateLobbyGameBoardIdCommand : IRequest
    {
        public string OwnerId { get; set; }

        public UpdateLobbyGameBoardIdCommand(string ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
