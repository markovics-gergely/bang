using Bang.DAL.Domain.Catalog.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DAL.Domain.Joins
{
    public class PlayerCard
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
