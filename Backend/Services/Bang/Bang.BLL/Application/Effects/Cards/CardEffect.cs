using Bang.DAL.Domain.Constants.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards
{
    public abstract class CardEffect
    {
        public abstract Task Execute(CardEffectQuery query, CancellationToken cancellationToken);
    }
}
