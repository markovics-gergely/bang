using System.Collections.Generic;

namespace Bang.BLL.Application.Commands.DataTransferObjects
{
    public class GameBoardDto
    {
        public int MaxTurnTime { get; set; }
        public string LobbyOwnerId { get; set; }
        public IEnumerable<UserDto> UserIds { get; set; }
    }
}
