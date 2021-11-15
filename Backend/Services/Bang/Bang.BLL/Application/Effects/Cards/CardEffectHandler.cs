using Bang.BLL.Application.Effects.Cards.CardEffects;
using Bang.BLL.Application.Interfaces;
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
            CardEffectMap = new Dictionary<CardType, CardEffect>
            {
                { CardType.Winchester, new WeaponCardEffect(5) },
                { CardType.Schofield, new WeaponCardEffect(2) },
                { CardType.Volcanic, new WeaponCardEffect(1) },
                { CardType.Remingtion, new WeaponCardEffect(3) },
                { CardType.Karabine, new WeaponCardEffect(4) },

                { CardType.Barrel, new PassiveCardEffect() },
                { CardType.Dynamite, new PassiveCardEffect() },
                { CardType.Mustang, new PassiveCardEffect() },
                { CardType.Scope, new PassiveCardEffect() },
                { CardType.Jail, new JailCardEffect() },

                { CardType.Missed, new MissedCardEffect() },
                { CardType.CatBalou, new CatBalouCardEffect() },
                { CardType.Panic, new PanicCardEffect() },
                { CardType.Stagecoach, new DrawCardCardEffect(2) },
                { CardType.WellsFargo, new DrawCardCardEffect(3) },
                { CardType.Bang, new BangCardEffect() },
                { CardType.Saloon, new SaloonCardEffect() },
                { CardType.Beer, new BeerCardEffect() },
                { CardType.Duel, new TargetPlayerCardEffect(TargetReason.Duel) },
                { CardType.GeneralStore, new GeneralStoreCardEffect() },
                { CardType.Gatling, new TargetNextPlayerCardEffect(TargetReason.Gatling) },
                { CardType.Indians, new TargetNextPlayerCardEffect(TargetReason.Indians) }
            };
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
