using Bang.DAL.Domain;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class PermissionViewModel
    {
        public bool CanDoAnything { get; set; } = true;
        public bool CanPlayCard { get; set; } = false;
        public bool CanDrawCard { get; set; } = false;
        public bool CanDiscardCard { get; set; } = false;
        public bool CanPlayBangCard { get; set; } = false;
        public bool CanLoseHealth { get; set; } = false;
        public bool CanPlayMissedCard { get; set; } = false;
        public bool CanDiscardFromDrawCard { get; set; } = false;
        public bool CanDrawFromDiscardCard { get; set; } = false;
        public bool CanTargetPlayers { get; set; } = false;
        public bool CanDrawFromMiddle { get; set; } = false;
        public bool CanSeeMiddleCards { get; set; } = false;
        public bool CanDrawFromOthersHands { get; set; } = false;
        public bool CanDrawFromOthersTable { get; set; } = false;
        public bool CanPlayBeerCard { get; set; } = false;
        public bool CanUseBarrelCard { get; set; } = false;

        public void SetByTargetReason(TargetReason? reason, Player targeted, Player actual)
        {
            if(reason == null) { return; }
            switch(reason)
            {
                case TargetReason.Bang:
                    CanLoseHealth = true;
                    CanUseBarrelCard = true;
                    CanPlayMissedCard = true;
                    if (targeted.CharacterType == CharacterType.CalamityJanet)
                    {
                        CanPlayBangCard = true;
                    }
                    break;
                case TargetReason.Duel:
                    CanLoseHealth = true;
                    CanPlayBangCard = true;
                    if (targeted.CharacterType == CharacterType.CalamityJanet)
                    {
                        CanPlayMissedCard = true;
                    }
                    break;
                case TargetReason.GeneralStore:
                    CanDrawFromMiddle = true;
                    break;
                case TargetReason.Indians:
                    CanPlayBangCard = true;
                    break;
            }
        }
    }
}
