using Bang.BLL.Application.Effects.Characters.CharacterEffects;
using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;

namespace Bang.BLL.Application.Effects.Characters
{
    public class CharacterEffectHandler
    {
        private static CharacterEffectHandler instance = null;

        public Dictionary<CharacterType, CharacterEffect> CharacterEffectMap { get; private set; }

        private CharacterEffectHandler()
        {
            CharacterEffectMap = new Dictionary<CharacterType, CharacterEffect>
            {
                { CharacterType.Jourdonnais, new JourdonnaisCharacterEffect() },
                { CharacterType.KitCarlson, new KitCarlsonCharacterEffect() },
                { CharacterType.SidKetchum, new SidKetchumCharacterEffect() }
            };
        }

        public static CharacterEffectHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CharacterEffectHandler();
                }
                return instance;
            }
        }
    }
}
