using Bang.BLL.Application.Interfaces;
using Bang.BLL.Application.Commands.Commands;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Constants;
using Bang.BLL.Application.Exceptions;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using Bang.BLL.Application.Commands.ViewModels;

using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

using AutoMapper;
using MediatR;
using Bang.DAL.Domain.Joins.PlayerCards;

namespace Bang.BLL.Application.Commands.Handlers
{
    public class GameBoardCommandHandler :
        IRequestHandler<CreateGameBoardCommand, long>,
        IRequestHandler<ShuffleGameBoardCardsCommand>,
        IRequestHandler<DiscardFromDrawableGameBoardCardCommand, FrenchCardViewModel>,
        IRequestHandler<EndGameBoardTurnCommand, Unit>,
        IRequestHandler<DeleteGameBoardCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IGameBoardStore _gameBoardStore;
        private readonly ICardStore _cardStore;
        private readonly IPlayerStore _playerStore;
        private readonly IRoleStore _roleStore;
        private readonly ICharacterStore _characterStore;
        private readonly IAccountStore _accountStore;

        public GameBoardCommandHandler(IMapper mapper, IGameBoardStore gameBoardStore, 
            ICardStore cardStore, IPlayerStore playerStore, IRoleStore roleStore, ICharacterStore characterStore,
            IAccountStore accountStore)
        {
            _mapper = mapper;
            _gameBoardStore = gameBoardStore;
            _cardStore = cardStore;
            _playerStore = playerStore;
            _roleStore = roleStore;
            _characterStore = characterStore;
            _accountStore = accountStore;
        }

        private static List<CardType> GetCardTypes()
        {
            var rnd = new Random();
            List<CardType> cardTypes = new List<CardType>();
            //Active cards
            cardTypes.AddRange(Enumerable.Repeat(CardType.Bang, 25).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Missed, 12).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Beer, 6).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Panic, 4).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.CatBalou, 4).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Duel, 3).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Stagecoach, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.GeneralStore, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Indians, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.WellsFargo, 1).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Gatling, 1).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Saloon, 1).ToList());
            //Passive Cards
            cardTypes.AddRange(Enumerable.Repeat(CardType.Schofield, 3).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Remingtion, 1).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Winchester, 1).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Karabine, 1).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Volcanic, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Mustang, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Jail, 3).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Barrel, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Dynamite, 1).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Scope, 1).ToList());
            cardTypes = cardTypes.OrderBy(c => rnd.Next()).ToList();
            return cardTypes;
        }

        public async Task<long> Handle(CreateGameBoardCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto.UserIds.Count() < 4)
                throw new NotEnoughPlayerException("Nem csatlakozott be elég játékos!");

            var domain = _mapper.Map<GameBoard>(request.Dto);
            long gameBoardId = await _gameBoardStore.CreateGameBoardAsync(domain, cancellationToken);

            var rnd = new Random();

            List<CardType> cardTypes = GetCardTypes();
            List<Card> cards = (List<Card>)await _cardStore.GetCardsAsync(cancellationToken);

            List<int> frenchNumbers = Enumerable.Range(1, 13).ToList();
            frenchNumbers.AddRange(Enumerable.Range(1, 7).ToList());
            var frenchCards = Enum.GetValues<CardColorType>().SelectMany(c => frenchNumbers, (c, n) => (Color: c, Number: n));
            frenchCards = frenchCards.OrderBy(f => rnd.Next());
            var frenchCardDataList = cardTypes.Zip(frenchCards, (c, f) => new { frenchCard = f, type = c }).ToList();
            
            //Players
            int playerCount = request.Dto.UserIds.Count();
            int outlawCount = playerCount > 5 ? 3 : 2;
            int viceCount = playerCount == 4 ? 0 : playerCount == 7 ? 2 : 1;

            var roles = new List<RoleType>();
            roles.Add(RoleType.Sheriff);
            roles.Add(RoleType.Renegade);
            roles.AddRange(Enumerable.Repeat(RoleType.Outlaw, outlawCount).ToList());
            roles.AddRange(Enumerable.Repeat(RoleType.Vice, viceCount).ToList());
            roles = roles.OrderBy(c => rnd.Next()).ToList();

            var characters = await _characterStore.GetCharactersAsync(cancellationToken);
            characters = characters.OrderBy(c => rnd.Next()).ToList().GetRange(0, playerCount);
            
            foreach (var userTuple in request.Dto.UserIds.OrderBy(order => rnd.Next()).Zip(characters, (u, c) => new { UserDto = u, Character = c })
                .Zip(roles, (uc, r) => new { UserDto = uc.UserDto, Character = uc.Character, RoleType = r }))
            {
                var player = new PlayerCreateViewModel()
                {
                    UserId = userTuple.UserDto.UserId,
                    UserName = userTuple.UserDto.UserName,
                    CharacterType = userTuple.Character.CharacterType,
                    RoleType = userTuple.RoleType,
                    GameBoardId = gameBoardId,
                    MaxHP = userTuple.Character.MaxHP + (userTuple.RoleType == RoleType.Sheriff ? 1 : 0),
                    ActualHP = userTuple.Character.MaxHP + (userTuple.RoleType == RoleType.Sheriff ? 1 : 0)
                };
                var playerDomain = _mapper.Map<Player>(player);
                long playerId = await _playerStore.CreatePlayerAsync(playerDomain, cancellationToken);

                if(playerDomain.RoleType == RoleType.Sheriff)
                {
                    domain.ActualPlayerId = playerId;
                }

                List<HandPlayerCard> playerCards = new List<HandPlayerCard>();
                var cardDataList = frenchCards.Zip(cardTypes, (f, c) => new { french = f, type = c });
                
                foreach (var cardTuple in frenchCardDataList.GetRange(0, playerDomain.MaxHP))
                {
                    Card card = cards.Where(c => c.CardType == cardTuple.type).FirstOrDefault();
                    var cardData = new PlayerCardViewModel()
                    {
                        CardId = card.Id,
                        PlayerId = playerDomain.Id, 
                        CardColorType = cardTuple.frenchCard.Color,
                        FrenchNumber = cardTuple.frenchCard.Number
                    };
                    playerCards.Add(_mapper.Map<HandPlayerCard>(cardData));
                }
                await _cardStore.CreatePlayerCardsAsync(playerCards, cancellationToken);
                frenchCardDataList.RemoveRange(0, playerDomain.MaxHP);
            }

            //Drawables
            List<GameBoardCard> cardDomainList = new List<GameBoardCard>();
            foreach (var cardTuple in frenchCardDataList)
            {
                Card card = cards.Where(c => c.CardType == cardTuple.type).FirstOrDefault();
                var cardData = new GameBoardCardViewModel()
                {
                    CardId = card.Id,
                    GameBoardId = gameBoardId,
                    StatusType = GameBoardCardConstants.DrawableCard,
                    CardColorType = cardTuple.frenchCard.Color,
                    FrenchNumber = cardTuple.frenchCard.Number
                };
                cardDomainList.Add(_mapper.Map<DrawableGameBoardCard>(cardData));
            }
            await _cardStore.CreateGameBoardCardsAsync(cardDomainList, cancellationToken);

            return gameBoardId;
        }

        public async Task<Unit> Handle(ShuffleGameBoardCardsCommand request, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var domain = await _gameBoardStore.GetGameBoardByUserAsync(userId, cancellationToken);
            await _gameBoardStore.ShuffleCardsAsync(domain, cancellationToken);

            return Unit.Value;
        }

        public async Task<FrenchCardViewModel> Handle(DiscardFromDrawableGameBoardCardCommand request, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var gameboard = await _gameBoardStore.GetGameBoardByUserAsync(userId, cancellationToken);

            if (gameboard.ActualPlayer.CharacterType == CharacterType.LuckyDuke)
            {
                var firstDomain = await _gameBoardStore.DiscardFromDrawableGameBoardCardAsync(cancellationToken);
                var secondDomain = await _gameBoardStore.DiscardFromDrawableGameBoardCardAsync(cancellationToken);
                if (gameboard.TurnPhase == PhaseEnum.Discarding)
                {
                    await _gameBoardStore.SetDiscardInDiscardingPhaseResultAsync(new DiscardedGameBoardCard[] { firstDomain, secondDomain}, gameboard, cancellationToken);
                }

                return _mapper.Map<FrenchCardViewModel>(secondDomain);
            }
            else
            {
                var domain = await _gameBoardStore.DiscardFromDrawableGameBoardCardAsync(cancellationToken);
                if (gameboard.TurnPhase == PhaseEnum.Discarding)
                {
                    await _gameBoardStore.SetDiscardInDiscardingPhaseResultAsync(domain, gameboard, cancellationToken);
                }

                return _mapper.Map<FrenchCardViewModel>(domain);
            }
            
        }

        public async Task<Unit> Handle(EndGameBoardTurnCommand request, CancellationToken cancellationToken)
        {
            await _gameBoardStore.EndGameBoardTurnAsync(cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteGameBoardCommand request, CancellationToken cancellationToken)
        {
            await _gameBoardStore.DeleteGameBoardAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
