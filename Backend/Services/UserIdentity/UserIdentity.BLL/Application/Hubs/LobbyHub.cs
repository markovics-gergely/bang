using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.BLL.Application.Interfaces.Hubs;

namespace UserIdentity.BLL.Application.Hubs
{
    [Authorize]
    public class LobbyHub : Hub<ILobbyHub>
    {
    }
}
