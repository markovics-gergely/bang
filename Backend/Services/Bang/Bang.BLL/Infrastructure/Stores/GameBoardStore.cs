using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Constants;
using MediatR;
using System;
using Bang.DAL.Domain.Joins.PlayerCards;
using Bang.BLL.Application.Effects.Cards;

namespace Bang.BLL.Infrastructure.Stores
{
    public class GameBoardStore : IGameBoardStore
    {
        private readonly BangDbContext _dbContext;
        private readonly IAccountStore _accountStore;
        private readonly IPlayerStore _playerStore;
        private readonly ICardStore _cardStore;

        public GameBoardStore(BangDbContext dbContext, IAccountStore accountStore, IPlayerStore playerStore, ICardStore cardStore)
        {
            _dbContext = dbContext;
            _accountStore = accountStore;
            _playerStore = playerStore;
            _cardStore = cardStore;
        }

        public async Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoards.AddAsync(gameBoard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoard.Id;
        }

        public async Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoardCards.AddAsync(gameBoardCard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoardCard.Id;
        }

        public async Task DeleteGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken)
        {
            GameBoardCard deletable = await GetGameBoardCardAsync(gameBoardCardId, cancellationToken);
            _dbContext.GameBoardCards.Remove(deletable);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoardCards.FirstOrDefaultAsync(p => p.Id == gameBoardCardId, cancellationToken) == null)
                    throw new EntityNotFoundException("GameBoardCard not found!");
                else throw;
            }
        }

        public async Task DeleteAllGameBoardCardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            List<GameBoardCard> deletables = await _dbContext.GameBoardCards.Where(c => c.GameBoardId == gameBoardId)
                .ToListAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoardCard not found!");
            foreach (GameBoardCard gameBoardCard in deletables)
            {
                _dbContext.GameBoardCards.Remove(gameBoardCard);
            }
            
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoardCards.Where(p => p.GameBoardId == gameBoardId).ToListAsync(cancellationToken) == null)
                    throw new EntityNotFoundException("GameBoardCard not found!");
                else throw;
            }
        }

        public async Task<DiscardedGameBoardCard> DiscardFromDrawableGameBoardCardAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            var domain = (await GetDrawableGameBoardCardsOnTopAsync(1, cancellationToken)).FirstOrDefault();
            var discarded = new DiscardedGameBoardCard()
            {
                CardId = domain.Card.Id,
                GameBoardId = gameBoard.Id, 
                CardColorType = domain.CardColorType,
                FrenchNumber = domain.FrenchNumber
            };
            await DeleteGameBoardCardAsync(domain.Id, cancellationToken);
            long newId = await CreateGameBoardCardAsync(discarded, cancellationToken);
            return (DiscardedGameBoardCard)await GetGameBoardCardAsync(newId, cancellationToken);
        }

        public async Task<IEnumerable<Card>> GetCardsOnTopAsync(int count, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);

            List<Card> drawables = new(gameBoard.DrawableGameBoardCards.Select(d => d.Card));
            return drawables.GetRange(drawables.Count - 1 - count, count);
        }

        public async Task<IEnumerable<DrawableGameBoardCard>> GetDrawableGameBoardCardsOnTopAsync(int count, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);

            List<DrawableGameBoardCard> drawables = (List<DrawableGameBoardCard>)gameBoard.DrawableGameBoardCards;
            if(drawables.Count < count)
            {
                count = drawables.Count;
                if (count == 0) return new List<DrawableGameBoardCard>();
            }
            return drawables.GetRange(drawables.Count - count, count);
        }

        public async Task<GameBoard> GetGameBoardAsync(long id, CancellationToken cancellationToken)
        {
            var aid = _accountStore.GetActualAccountId();
            Console.WriteLine(aid);
            return await _dbContext.GameBoards.Where(c => c.Id == id)
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards).ThenInclude(c => (c as HandPlayerCard).Card)
                .Include(g => g.Players).ThenInclude(p => p.TablePlayerCards).ThenInclude(c => (c as TablePlayerCard).Card)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoard not found!");
        }

        public async Task<GameBoardCard> GetGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoardCards.Where(c => c.Id == gameBoardCardId)
                .Include(g => g.Card).FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("GameBoardCard not found!");
        }

        public async Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoards
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards)
                .ToListAsync(cancellationToken);
        }

        public async Task<Card> GetLastDiscardedCardAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);

            if (gameBoard.DiscardedGameBoardCards.Count == 0) 
                throw new EntityNotFoundException("Card not found!");
            return gameBoard.DiscardedGameBoardCards.Last().Card;
        }

        public async Task ShuffleCardsAsync(GameBoard gameBoard, CancellationToken cancellationToken)
        {
            List<DiscardedGameBoardCard> deletables = (List<DiscardedGameBoardCard>)gameBoard.DiscardedGameBoardCards;
            List<DrawableGameBoardCard> drawables = new List<DrawableGameBoardCard>();
            foreach (DiscardedGameBoardCard gameBoardCard in deletables)
            {
                DrawableGameBoardCard drawable = new DrawableGameBoardCard()
                {
                    CardId = gameBoardCard.Card.Id,
                    GameBoardId = gameBoard.Id,
                    StatusType = GameBoardCardConstants.DrawableCard,
                    CardColorType = gameBoardCard.CardColorType, 
                    FrenchNumber = gameBoardCard.FrenchNumber
                };
                drawables.Add(drawable);
                await DeleteGameBoardCardAsync(gameBoard.Id, cancellationToken);
            }
            var rnd = new Random();
            drawables = drawables.OrderBy(d => rnd.Next()).ToList();
            foreach (DrawableGameBoardCard drawable in drawables)
            {
                await CreateGameBoardCardAsync(drawable, cancellationToken);
            }
        }

        public async Task<DiscardedGameBoardCard> GetLastDiscardedGameBoardCardAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);

            if (gameBoard.DiscardedGameBoardCards.Count == 0)
                throw new EntityNotFoundException("Card not found!");
            return gameBoard.DiscardedGameBoardCards.Last();
        }

        public async Task SetGameBoardEndAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.IsOver = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await DeleteAllGameBoardCardAsync(gameBoard.Id, cancellationToken);
        }

        public async Task<GameBoard> GetGameBoardByUserAsync(string userId, CancellationToken cancellationToken)
        {
            Player player = await _dbContext.Players.Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Player not found!");
            return await _dbContext.GameBoards.Where(g => g.Players.Contains(player))
                .Include(g => g.Players).ThenInclude(p => p.TablePlayerCards).ThenInclude(table => table.Card)
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards).ThenInclude(hand => hand.Card)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoard not found!");
        }

        public async Task SetGameBoardActualPlayerAsync(long playerId, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.ActualPlayerId = playerId;
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoard.Id) == null)
                        throw new EntityNotFoundException("Nem található a gameboard");
                else if (await _dbContext.Players
                    .SingleOrDefaultAsync(p => p.Id == playerId) == null)
                        throw new EntityNotFoundException("Nem található a player");
                else throw;
            }
        }

        public async Task SetGameBoardTargetedPlayerAsync(long? playerId, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.TargetedPlayerId = playerId;
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoard.Id) == null)
                    throw new EntityNotFoundException("Nem található a gameboard");
                else if (await _dbContext.Players
                    .SingleOrDefaultAsync(p => p.Id == playerId) == null)
                    throw new EntityNotFoundException("Nem található a player");
                else throw;
            }
        }

        public async Task SetGameBoardPhaseAsync(PhaseEnum phaseEnum, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.TurnPhase = phaseEnum;
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoard.Id) == null)
                    throw new EntityNotFoundException("Nem található a gameboard");
                else throw;
            }
        }

        public async Task SetGameBoardTargetReasonAsync(TargetReason? targetReason, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.TargetReason = targetReason;
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoard.Id) == null)
                    throw new EntityNotFoundException("Nem található a gameboard");
                else throw;
            }
        }

        public async Task EndGameBoardTurnAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            var cards = gameBoard.ActualPlayer.HandPlayerCards;
            if (cards.Count > gameBoard.ActualPlayer.ActualHP)
            {
                var rnd = new Random();
                var throwable = cards.OrderBy(x => rnd.Next()).Take(cards.Count - gameBoard.ActualPlayer.ActualHP);
                foreach (var card in throwable)
                {
                    await _cardStore.PlacePlayerCardToDiscardedAsync(card, cancellationToken);
                }
            }
            await _playerStore.DeletePlayerPlayedCardAsync(cancellationToken);
            gameBoard.TurnPhase = PhaseEnum.Drawing;

            var nextPlayer = await _playerStore.GetNextPlayerAliveByPlayerAsync((long)gameBoard.ActualPlayerId, cancellationToken);
            gameBoard.ActualPlayer = nextPlayer;
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoard.Id) == null)
                    throw new EntityNotFoundException("Nem található a gameboard");
                else throw;
            }
        }

        public async Task<long> DrawGameBoardCardAsync(long gameBoardCardId, long playerId, CancellationToken cancellationToken)
        {
            var gameBoardCard = await GetGameBoardCardAsync(gameBoardCardId, cancellationToken);
            HandPlayerCard drawnCard = new HandPlayerCard()
            {
                PlayerId = playerId,
                CardId = gameBoardCard.CardId,
                CardColorType = gameBoardCard.CardColorType,
                FrenchNumber = gameBoardCard.FrenchNumber
            };
            await DeleteGameBoardCardAsync(gameBoardCardId, cancellationToken);
            return await _cardStore.CreatePlayerCardAsync(drawnCard, cancellationToken);
        }

        public async Task DrawGameBoardCardsFromTopAsync(int count, long playerId, CancellationToken cancellationToken)
        {
            List<DrawableGameBoardCard> drawables = (List<DrawableGameBoardCard>)await GetGameBoardCardsOnTopAsync(count, cancellationToken);
            foreach (var gameBoardCard in drawables)
            {
                await DrawGameBoardCardAsync(gameBoardCard.Id, playerId, cancellationToken);
            }
        }

        public async Task DrawGameBoardCardsFromTopAsync(int count, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var player = await _playerStore.GetPlayerByUserIdAsync(userId, cancellationToken);
            List<DrawableGameBoardCard> drawables = (List<DrawableGameBoardCard>)await GetGameBoardCardsOnTopAsync(count, cancellationToken);
            foreach (var gameBoardCard in drawables)
            {
                await DrawGameBoardCardAsync(gameBoardCard.Id, player.Id, cancellationToken);
            }
        }

        public async Task<IEnumerable<DrawableGameBoardCard>> GetGameBoardCardsOnTopAsync(int count, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);

            List<DrawableGameBoardCard> drawables = (List<DrawableGameBoardCard>)gameBoard.DrawableGameBoardCards;
            return drawables.GetRange(drawables.Count - 1 - count, count);
        }

        public async Task PlayCardAsync(long playerCardId, CancellationToken cancellationToken)
        {
            HandPlayerCard playerCard = (HandPlayerCard)await _cardStore.GetPlayerCardAsync(playerCardId, cancellationToken);
            Dictionary<CardType, CardEffect> cardEffectMap = CardEffectHandler.Instance.CardEffectMap;

            var effect = cardEffectMap[playerCard.Card.CardType] ?? throw new EntityNotFoundException("Nem található a cardeffect!");
            CardEffectQuery query = new CardEffectQuery(playerCard, this, _cardStore, _playerStore, _accountStore);
            await effect.Execute(query, cancellationToken);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.PlayerCards
                    .SingleOrDefaultAsync(p => p.Id == playerCardId) == null)
                    throw new EntityNotFoundException("Nem található a playercard");
                else throw;
            }
            await _playerStore.AddPlayedCardAsync(playerCard.Card.CardType, cancellationToken);
            await SetGameBoardPhaseAsync(PhaseEnum.Playing, cancellationToken);
        }

        public async Task PlayCardAsync(long playerCardId, long targetPlayerCardId, bool isTargetPlayer, CancellationToken cancellationToken)
        {
            HandPlayerCard playerCard = (HandPlayerCard)await _cardStore.GetPlayerCardAsync(playerCardId, cancellationToken);
            Dictionary<CardType, CardEffect> cardEffectMap = CardEffectHandler.Instance.CardEffectMap;

            var effect = cardEffectMap[playerCard.Card.CardType] ?? throw new EntityNotFoundException("Nem található a cardeffect!");
            CardEffectQuery query;
            if (isTargetPlayer)
            {
                var target = await _playerStore.GetPlayerAsync(targetPlayerCardId, cancellationToken);
                query = new CardEffectQuery(playerCard, target, this, _cardStore, _playerStore, _accountStore);
            }
            else
            {
                var target = await _cardStore.GetPlayerCardAsync(targetPlayerCardId, cancellationToken);
                query = new CardEffectQuery(playerCard, target, this, _cardStore, _playerStore, _accountStore);
            }
            await effect.Execute(query, cancellationToken);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.PlayerCards
                    .SingleOrDefaultAsync(p => p.Id == playerCardId) == null)
                    throw new EntityNotFoundException("Nem található a playercard");
                else throw;
            }
            await _playerStore.AddPlayedCardAsync(playerCard.Card.CardType, cancellationToken);
            await SetGameBoardPhaseAsync(PhaseEnum.Playing, cancellationToken);
        }
    }
}
