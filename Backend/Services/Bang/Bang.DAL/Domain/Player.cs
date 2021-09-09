using Bang.DAL.Domain.Joins;

using System;
using System.Collections.Generic;

namespace Bang.DAL.Domain
{
    public class Player
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long GameBoardId { get; set; }
        public GameBoard GameBoard { get; set; }
        public string CharacterType { get; set; }
        public string RoleType { get; set; }
        private int actualHP;
        public int ActualHP {
            get => actualHP;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Nem lehet negatív életet megadni");
                }
                actualHP = value;
            }
        }
        public int MaxHP { get; init; }

        public ICollection<PlayerCard> PlayerCards { get; set; }
    }
}
