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
            CardEffectMap.Add(CardType.Bang, new BangCardEffect());
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
