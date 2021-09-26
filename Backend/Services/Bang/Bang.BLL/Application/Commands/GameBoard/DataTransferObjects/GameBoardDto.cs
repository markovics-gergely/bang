using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Commands.DataTransferObjects
{
    public class GameBoardDto
    {
        public int MaxTurnTime { get; set; }
        public List<UserDto> UserIds { get; set; }
    }
}
