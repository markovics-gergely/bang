using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class WeaponCardEffect : PassiveCardEffect
    {
        public int Range { get; set; }
        
        public WeaponCardEffect(int range)
        {
            Range = range;
        }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            query.PlayerCard.Player.ShootingRange = Range;
            await base.Execute(query, cancellationToken);
        }
    }
}
