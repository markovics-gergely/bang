using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards
{
    public abstract class CardEffect
    {
        public abstract Task Execute(CardEffectQuery query);
    }
}
