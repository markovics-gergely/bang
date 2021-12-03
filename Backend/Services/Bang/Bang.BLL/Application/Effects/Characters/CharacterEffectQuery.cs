using Bang.BLL.Application.Commands.Player.DataTransferObjects;
using Bang.BLL.Application.Interfaces;

namespace Bang.BLL.Application.Effects.Characters
{
    public class CharacterEffectQuery
    {
        public CharacterDto CharacterDto { get; set; }

        public IGameBoardStore GameBoardStore { get; init; }
        public ICardStore CardStore { get; init; }
        public IPlayerStore PlayerStore { get; init; }
        public IAccountStore AccountStore { get; init; }

        public CharacterEffectQuery(CharacterDto characterDto, IGameBoardStore gameBoardStore, ICardStore cardStore,
            IPlayerStore playerStore, IAccountStore accountStore)
        {
            CharacterDto = characterDto;
            GameBoardStore = gameBoardStore;
            CardStore = cardStore;
            PlayerStore = playerStore;
            AccountStore = accountStore;
        }
    }
}
