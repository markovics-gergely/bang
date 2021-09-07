using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Joins.GameBoardCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DAL.Domain
{
    public class GameBoard
    {
        public int Id { get; set; }
        public int ActualPlayerId { get; set; }
        public Player ActualPlayer { get; set; }
        public int MaxTurnTime { get; set; }
        public bool IsOver { get; set; } = false;

        public ICollection<Player> Players;
        public ICollection<DrawableGameBoardCard> DrawableGameBoardCards;
        public ICollection<DiscardedGameBoardCard> DiscardedGameBoardCards;

        public ICollection<Card> DrawableCards;
        public ICollection<Card> DiscardedCards;
    }
}
