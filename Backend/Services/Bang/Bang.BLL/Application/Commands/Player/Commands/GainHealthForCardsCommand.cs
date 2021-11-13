using MediatR;
using System.Collections.Generic;

namespace Bang.BLL.Application.Commands.Commands
{
    public class GainHealthForCardsCommand : IRequest
    {
        public IEnumerable<long> Cards { get; set; }

        public GainHealthForCardsCommand(IEnumerable<long> cards)
        {
            Cards = cards;
        }
    }
}
