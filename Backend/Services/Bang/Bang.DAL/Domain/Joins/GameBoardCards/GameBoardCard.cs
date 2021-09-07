using Bang.DAL.Domain.Catalog.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DAL.Domain.Joins
{
    public class GameBoardCard
    {
        public int Id { get; set; }

        public int GameBoardId { get; set; }
        public GameBoard GameBoard { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public string StatusType { get; set; }
    }
}
