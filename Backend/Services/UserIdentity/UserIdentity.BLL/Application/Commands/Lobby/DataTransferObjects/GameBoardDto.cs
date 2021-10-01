using System.Collections.Generic;

namespace UserIdentity.BLL.Application.Commands.DataTransferObjects
{
    public class GameBoardDto
    {
        public int MaxTurnTime { get; set; }
        public IEnumerable<UserDto> UserIds { get; set; }
    }
}
