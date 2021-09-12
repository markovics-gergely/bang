using System;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards
{
    public class BangCardEffect : CardEffect
    {
        public override async Task Execute(CardEffectQuery query)
        {
            BangCardEffectQuery bangQuery = (BangCardEffectQuery)query;
            // TODO
            throw new NotImplementedException();
        }
    }
}
