using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class DrawCardCardEffect : ActiveCardEffect
    {
        public int Count { get; set; }
        
        public DrawCardCardEffect(int count)
        {
            Count = count;
        }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            await query.GameBoardStore.DrawGameBoardCardsFromTopAsync(Count, cancellationToken);
            await base.Execute(query, cancellationToken);
        }
    }
}
