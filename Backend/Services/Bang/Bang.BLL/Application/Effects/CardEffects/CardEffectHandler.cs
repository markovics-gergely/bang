using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;

namespace Bang.BLL.Application.Effects.CardEffects
{
    public class CardEffectHandler
    {
        private static CardEffectHandler instance = null;

        public Dictionary<ActiveCardType, ActiveCardEffect> ActiveEffectMap { get; private set; }
        public Dictionary<ActiveCardType, ActiveCardEffect> PassiveEffectMap { get; private set; }

        private CardEffectHandler()
        {
            ActiveEffectMap = new Dictionary<ActiveCardType, ActiveCardEffect>();
            ActiveEffectMap.Add(ActiveCardType.Bang, new BangActiveCardEffect());
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
