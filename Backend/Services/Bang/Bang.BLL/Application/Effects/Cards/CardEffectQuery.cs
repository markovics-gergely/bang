using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.PlayerCards;

namespace Bang.BLL.Application.Effects.Cards
{
    public class CardEffectQuery
    {
        public HandPlayerCard PlayerCard { get; init; }
        public Player TargetPlayer { get; init; }
        public PlayerCard TargetCard { get; init; }
        public IGameBoardStore GameBoardStore { get; init; }
        public ICardStore CardStore { get; init; }
        public IPlayerStore PlayerStore { get; init; }
        public IAccountStore AccountStore { get; init; }

        public CardEffectQuery(HandPlayerCard playerCard, IGameBoardStore gameBoardStore, ICardStore cardStore,
            IPlayerStore playerStore, IAccountStore accountStore)
        {
            PlayerCard = playerCard;
            GameBoardStore = gameBoardStore;
            CardStore = cardStore;
            PlayerStore = playerStore;
            AccountStore = accountStore;
        }

        public CardEffectQuery(HandPlayerCard playerCard, Player targetPlayer, IGameBoardStore gameBoardStore, ICardStore cardStore,
            IPlayerStore playerStore, IAccountStore accountStore)
        {
            PlayerCard = playerCard;
            TargetPlayer = targetPlayer;
            GameBoardStore = gameBoardStore;
            CardStore = cardStore;
            PlayerStore = playerStore;
            AccountStore = accountStore;
        }

        public CardEffectQuery(HandPlayerCard playerCard, PlayerCard targetCard, IGameBoardStore gameBoardStore, ICardStore cardStore,
            IPlayerStore playerStore, IAccountStore accountStore)
        {
            PlayerCard = playerCard;
            TargetCard = targetCard;
            GameBoardStore = gameBoardStore;
            CardStore = cardStore;
            PlayerStore = playerStore;
            AccountStore = accountStore;
        }
    }
}
