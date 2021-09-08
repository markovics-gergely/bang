using Bang.DAL.Domain.Catalog.Cards;

namespace Bang.DAL.Domain.Joins.GameBoardCards
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
