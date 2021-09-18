using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;

using System;

namespace Bang.DAL.Domain.Joins
{
    public class PlayerCard
    {
        public long Id { get; set; }

        public long PlayerId { get; set; }
        public Player Player { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public string StatusType { get; set; }

        public CardColorType CardColorType { get; set; }
        private int frenchNumber;
        public int FrenchNumber
        {
            get => frenchNumber;
            set
            {
                if (value < 1 || value > 13)
                {
                    throw new ArgumentOutOfRangeException("Nem értelmes kártyaszám!");
                }
                frenchNumber = value;
            }
        }
    }
}
