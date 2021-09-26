using Bang.BLL.Application.Effects.Cards.CardEffectQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class WeaponCardEffect : CardEffect
    {
        public int Range { get; set; }
        
        public WeaponCardEffect(int range)
        {
            Range = range;
        }

        public override async Task Execute(CardEffectQuery query)
        {
            query.Player.ShootingRange = Range;
        }
    }
}
