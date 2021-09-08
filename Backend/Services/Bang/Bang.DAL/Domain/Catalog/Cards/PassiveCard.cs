using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DAL.Domain.Catalog.Cards
{
    public class PassiveCard : Card
    {
        public PassiveCardType CardType { get; set; }
    }
}
