using Bang.BLL.Application.Effects.Cards.CardEffects;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;

namespace Bang.BLL.Application.Effects.Cards
{
    public class CardEffectHandler
    {
        private static CardEffectHandler instance = null;

        public Dictionary<CardType, CardEffect> CardEffectMap { get; private set; }

        private CardEffectHandler()
        {
            CardEffectMap = new Dictionary<CardType, CardEffect>();

            CardEffectMap.Add(CardType.Winchester, new WeaponCardEffect(5));
            CardEffectMap.Add(CardType.Schofield, new WeaponCardEffect(2));
            CardEffectMap.Add(CardType.Volcanic, new WeaponCardEffect(1));
            CardEffectMap.Add(CardType.Remingtion, new WeaponCardEffect(3));
            CardEffectMap.Add(CardType.Karabine, new WeaponCardEffect(4));
        }

        public static CardEffectHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CardEffectHandler();
                }
                return instance;
            }
        }
    }
}
