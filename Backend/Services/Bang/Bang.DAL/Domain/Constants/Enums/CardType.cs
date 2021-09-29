namespace Bang.DAL.Domain.Constants.Enums
{
    public enum CardType
    {
        Bang,
        Missed,
        Beer,
        CatBalou,
        Panic,
        Duel,
        GeneralStore,
        Indians,
        Stagecoach,
        Gatling,
        Saloon,
        WellsFargo,
        Jail,
        Mustang,
        Barrel,
        Scope,
        Dynamite,
        Volcanic,
        Schofield,
        Remingtion,
        Karabine,
        Winchester
    }

    public static class CardTypeMethods
    {
        public static bool IsWeapon(this CardType type)
        {
            switch(type)
            {
                case CardType.Schofield:
                case CardType.Remingtion:
                case CardType.Volcanic:
                case CardType.Karabine: return true;
                default: return false;
            }
        }

        public static bool IsRangeModifier(this CardType type)
        {
            switch (type)
            {
                case CardType.Scope:
                case CardType.Mustang: return true;
                default: return false;
            }
        }

        public static bool NeedsTarget(this CardType type)
        {
            switch (type)
            {
                case CardType.Jail: return true;
                default: return false;
            }
        }
    }
}
