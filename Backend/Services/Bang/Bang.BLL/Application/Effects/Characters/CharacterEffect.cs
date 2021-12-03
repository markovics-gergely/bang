using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Characters
{
    public abstract class CharacterEffect
    {
        public abstract Task Execute(CharacterEffectQuery query, CancellationToken cancellationToken);
    }
}
