using Bang.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffectQueries
{
    public class WeaponCardEffectQuery : CardEffectQuery
    {
        public Player Player { get; set; }

        public WeaponCardEffectQuery(Player player)
        {
            Player = player;
        }
    }
}
