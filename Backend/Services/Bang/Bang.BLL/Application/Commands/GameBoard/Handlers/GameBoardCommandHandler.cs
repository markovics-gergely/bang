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

namespace Bang.BLL.Application.Commands.Handlers
{
    public class GameBoardCommandHandler :
        IRequestHandler<CreateGameBoardCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IGameBoardStore _gameBoardStore;
        private readonly ICardStore _cardStore;
        private readonly IPlayerStore _playerStore;
        private readonly IRoleStore _roleStore;
        private readonly ICharacterStore _characterStore;

        public GameBoardCommandHandler(IMapper mapper, IGameBoardStore gameBoardStore, 
            ICardStore cardStore, IPlayerStore playerStore, IRoleStore roleStore, ICharacterStore characterStore)
        {
            _mapper = mapper;
            _gameBoardStore = gameBoardStore;
            _cardStore = cardStore;
            _playerStore = playerStore;
            _roleStore = roleStore;
            _characterStore = characterStore;
        }

        public async Task<long> Handle(CreateGameBoardCommand request, CancellationToken cancellationToken)
        {
            if(request.Dto.UserIds.Count < 4)
                throw new NotEnoughPlayerException("Nem csatlakozott be elég játékos!");

            var domain = _mapper.Map<GameBoard>(request.Dto);
            long gameBoardId = await _gameBoardStore.CreateGameBoardAsync(domain, cancellationToken);

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
            //TODO fegyver ?
            //TODO ló ?
            cardTypes.AddRange(Enumerable.Repeat(CardType.Jail, 3).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Barrel, 2).ToList());
            cardTypes.AddRange(Enumerable.Repeat(CardType.Dynamite, 1).ToList());
            cardTypes = cardTypes.OrderBy(c => rnd.Next()).ToList();
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
            List<Card> cards = (List<Card>)await _cardStore.GetCardsAsync(cancellationToken);

            //Players
            int playerCount = request.Dto.UserIds.Count;
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

            foreach (var userTuple in request.Dto.UserIds.Zip(characters, (u, c) => new { uId = u, Character = c })
                .Zip(roles, (uc, r) => new { UserId = uc.uId, Character = uc.Character, RoleType = r }))
            {
                var player = new PlayerCreateViewModel()
                {
                    UserId = userTuple.UserId,
                    CharacterType = userTuple.Character.CharacterType,
                    RoleType = userTuple.RoleType,
                    GameBoardId = gameBoardId,
                    MaxHP = userTuple.Character.MaxHP,
                    ActualHP = userTuple.Character.MaxHP
                };
                var playerDomain = _mapper.Map<Player>(player);
                await _playerStore.CreatePlayerAsync(playerDomain, cancellationToken);

                List<PlayerCard> playerCards = new List<PlayerCard>();
                foreach (CardType type in cardTypes.GetRange(0, playerDomain.MaxHP))
                {
                    Card card = cards.Where(c => c.CardType == type).FirstOrDefault();
                    var cardData = new PlayerCardViewModel()
                    {
                        CardId = card.Id,
                        PlayerId = playerDomain.Id
                    };
                    playerCards.Add(_mapper.Map<PlayerCard>(cardData));
                }
                await _cardStore.CreatePlayerCardsAsync(playerCards, cancellationToken);
                cardTypes.RemoveRange(0, playerDomain.MaxHP);
            }

            //Drawables
            List<GameBoardCard> cardDomainList = new List<GameBoardCard>();
            foreach (CardType type in cardTypes)
            {
                Card card = cards.Where(c => c.CardType == type).FirstOrDefault();
                var cardData = new GameBoardCardViewModel()
                {
                    CardId = card.Id,
                    GameBoardId = gameBoardId,
                    StatusType = GameBoardCardConstants.DrawableCard
                };
                cardDomainList.Add(_mapper.Map<DrawableGameBoardCard>(cardData));
            }
            await _cardStore.CreateGameBoardCardsAsync(cardDomainList, cancellationToken);

            
            return gameBoardId;
        }
    }
}
