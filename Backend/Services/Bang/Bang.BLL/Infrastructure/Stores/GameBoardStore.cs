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
            if (await _dbContext.GameBoards.FirstOrDefaultAsync(g => g.LobbyOwnerId == gameBoard.LobbyOwnerId, cancellationToken) != null)
                throw new EntityAlreadyExistException("Lobby already has a gameboard!");

            await _dbContext.GameBoards.AddAsync(gameBoard, cancellationToken);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards.FirstOrDefaultAsync(g => g.Id == gameBoard.Id, cancellationToken) == null)
                    throw new EntityNotFoundException("GameBoard not found!");
                else throw;
            }
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
                .Include(g => g.ScatteredGameBoardCards).ThenInclude(s => s.Card)
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
            foreach (Player player in gameBoard.Players)
            {
                await _cardStore.DeleteAllPlayerCardAsync(player.Id, cancellationToken);
            }
            await DeleteAllGameBoardCardAsync(gameBoard.Id, cancellationToken);

            gameBoard.IsOver = true;
            gameBoard.ActualPlayerId = null;
            gameBoard.TargetedPlayerId = null;
            gameBoard.TargetReason = null;
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

        public async Task SetGameBoardEndAsync(long id, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(id, cancellationToken);
            foreach (Player player in gameBoard.Players)
            {
                await _cardStore.DeleteAllPlayerCardAsync(player.Id, cancellationToken);
            }
            await DeleteAllGameBoardCardAsync(gameBoard.Id, cancellationToken);

            gameBoard.IsOver = true;
            gameBoard.ActualPlayerId = null;
            gameBoard.TargetedPlayerId = null;
            gameBoard.TargetReason = null;
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
                .Include(g => g.ScatteredGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.ActualPlayer).ThenInclude(p => p.TablePlayerCards).ThenInclude(table => table.Card)
                .Include(g => g.ActualPlayer).ThenInclude(p => p.HandPlayerCards).ThenInclude(hand => hand.Card)
                .Include(g => g.TargetedPlayer).ThenInclude(p => p.TablePlayerCards).ThenInclude(table => table.Card)
                .Include(g => g.TargetedPlayer).ThenInclude(p => p.HandPlayerCards).ThenInclude(hand => hand.Card)
                .Include(g => g.ScatteredGameBoardCards).ThenInclude(s => s.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoard not found!");
        }

        public async Task<GameBoard> GetGameBoardByUserSimplifiedAsync(string userId, CancellationToken cancellationToken)
        {
            Player player = await _dbContext.Players.Where(c => c.UserId == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Player not found!");
            return await _dbContext.GameBoards.Where(g => g.Players.Contains(player))
                .Include(g => g.Players).ThenInclude(p => p.TablePlayerCards).ThenInclude(table => table.Card)
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards).ThenInclude(hand => hand.Card)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.ScatteredGameBoardCards).ThenInclude(s => s.Card)
                .AsNoTracking()
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

            var needsDiscard = nextPlayer.TablePlayerCards.Select(t => t.Card.CardType)
                .Where(c => c == CardType.Dynamite || c == CardType.Jail).ToList();
            if (needsDiscard.Count == 2)
            {
                if (needsDiscard.ElementAtOrDefault(0) == CardType.Jail && needsDiscard.ElementAtOrDefault(1) == CardType.Dynamite)
                {
                    gameBoard.TargetReason = TargetReason.JailAndDynamite;
                } 
                 else if (needsDiscard.ElementAtOrDefault(0) == CardType.Dynamite && needsDiscard.ElementAtOrDefault(1) == CardType.Jail) {
                    gameBoard.TargetReason = TargetReason.DynamiteAndJail;
                }
                gameBoard.TurnPhase = PhaseEnum.Discarding;
            }
            if (needsDiscard.Count == 1)
            {
                if (needsDiscard.ElementAtOrDefault(0) == CardType.Jail)
                {
                    gameBoard.TargetReason = TargetReason.Jail;
                }
                else if (needsDiscard.ElementAtOrDefault(0) == CardType.Dynamite)
                {
                    gameBoard.TargetReason = TargetReason.Dynamite;
                }
                gameBoard.TurnPhase = PhaseEnum.Discarding;
            }
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

        public async Task<long> DrawGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var player = await _playerStore.GetPlayerByUserIdAsync(userId, cancellationToken);
            var gameBoardCard = await GetGameBoardCardAsync(gameBoardCardId, cancellationToken);
            HandPlayerCard drawnCard = new HandPlayerCard()
            {
                PlayerId = player.Id,
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
            var query = new CardEffectQuery(playerCard, this, _cardStore, _playerStore, _accountStore);
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
            var userId = _accountStore.GetActualAccountId();
            var gameboard = await GetGameBoardByUserSimplifiedAsync(userId, cancellationToken);
            if (gameboard.TargetedPlayerId != playerCard.PlayerId)
            {
                await _playerStore.AddPlayedCardAsync(playerCard.Card.CardType, cancellationToken);
                await SetGameBoardPhaseAsync(PhaseEnum.Playing, cancellationToken);
            }
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
            var userId = _accountStore.GetActualAccountId();
            var gameboard = await GetGameBoardByUserSimplifiedAsync(userId, cancellationToken);
            if (gameboard.TargetedPlayerId != playerCard.PlayerId)
            {
                await _playerStore.AddPlayedCardAsync(playerCard.Card.CardType, cancellationToken);
                await SetGameBoardPhaseAsync(PhaseEnum.Playing, cancellationToken);
            }
        }

        public async Task DrawGameBoardCardsToScatteredAsync(int count, CancellationToken cancellationToken)
        {
            var cardsOnTop = await GetGameBoardCardsOnTopAsync(count, cancellationToken);
            foreach (var card in cardsOnTop)
            {
                ScatteredGameBoardCard newCard = new ScatteredGameBoardCard()
                {
                    GameBoardId = card.GameBoardId,
                    CardId = card.CardId,
                    CardColorType = card.CardColorType,
                    FrenchNumber = card.FrenchNumber
                };
                await DeleteGameBoardCardAsync(card.Id, cancellationToken);
                await _cardStore.CreateGameBoardCardAsync(newCard, cancellationToken);
            }
        }

        public async Task DrawGameBoardCardsToScatteredByPlayersAliveAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var gameboard = await GetGameBoardByUserAsync(userId, cancellationToken);
            var aliveCount = await _playerStore.GetRemainingPlayerCountAsync(gameboard.Id, cancellationToken);
            await DrawGameBoardCardsToScatteredAsync(aliveCount, cancellationToken);
        }

        public async Task<bool> CalculatePlayerPlacementAsync(long deadPlayerId, CancellationToken cancellationToken)
        {
            var deadPlayer = await _playerStore.GetPlayerAsync(deadPlayerId, cancellationToken);
            var players = await _playerStore.GetPlayersByGameBoardAsync(deadPlayer.GameBoardId, cancellationToken);
            players = players.Where(p => p.Id != deadPlayerId);
            var alivePlayers = players.Where(p => p.ActualHP > 0);
            var deadPlayers = players.Where(p => p.ActualHP == 0);

            var outlaws = players.Where(p => p.RoleType == RoleType.Outlaw);
            var vices = players.Where(p => p.RoleType == RoleType.Vice);
            var renegade = players.FirstOrDefault(d => d.RoleType == RoleType.Renegade);
            var sheriff = players.FirstOrDefault(d => d.RoleType == RoleType.Sheriff);

            bool isCalculated = false;

            switch (deadPlayer.RoleType)
            {
                case RoleType.Outlaw:
                    if (alivePlayers.FirstOrDefault(d => d.RoleType == RoleType.Outlaw) == null && deadPlayers.Contains(renegade))
                    {
                        renegade.Placement = 3;
                        deadPlayer.Placement = 2;
                        foreach (var outlaw in outlaws)
                        {
                            outlaw.Placement = 2;
                        }
                        foreach (var vice in vices)
                        {
                            vice.Placement = 1;
                        }
                        sheriff.Placement = 1;
                        isCalculated = true;
                    }
                    break;
                case RoleType.Renegade:
                    if (alivePlayers.FirstOrDefault(d => d.RoleType == RoleType.Outlaw) == null)
                    {
                        deadPlayer.Placement = 2;
                        foreach (var outlaw in outlaws)
                        {
                            outlaw.Placement = 3;
                        }
                        foreach (var vice in vices)
                        {
                            vice.Placement = 1;
                        }
                        sheriff.Placement = 1;
                        isCalculated = true;
                    }
                    break;
                case RoleType.Sheriff:
                    if (alivePlayers.FirstOrDefault(p => p.RoleType == RoleType.Outlaw) != null)
                    {
                        foreach (var outlaw in outlaws)
                        {
                            outlaw.Placement = 1;
                        }
                        if (deadPlayers.Contains(renegade))
                        {
                            foreach (var vice in vices)
                            {
                                vice.Placement = 2;
                            }
                            deadPlayer.Placement = 2;
                            renegade.Placement = 3;
                        }
                        else
                        {
                            foreach (var vice in vices)
                            {
                                vice.Placement = 3;
                            }
                            deadPlayer.Placement = 3;
                            renegade.Placement = 2;
                        }
                    }
                    else
                    {
                        renegade.Placement = 1;
                        foreach (var outlaw in outlaws)
                        {
                            outlaw.Placement = 2;
                        }
                        deadPlayer.Placement = 3;
                        foreach (var vice in vices)
                        {
                            vice.Placement = 3;
                        }
                    }
                    isCalculated = true;
                    break;
                case RoleType.Vice:
                    break;
            }
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                isCalculated = false;
                if (await _dbContext.PlayerCards
                    .SingleOrDefaultAsync(p => p.Id == deadPlayerId) == null)
                    throw new EntityNotFoundException("Nem található a playercard");
                else throw;
            }
            return isCalculated;
        }

        public async Task DeleteGameBoardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            await SetGameBoardEndAsync(gameBoardId, cancellationToken);
            var gameboard = await GetGameBoardAsync(gameBoardId, cancellationToken);
            var players = gameboard.Players;
            foreach (var player in players)
            {
                _dbContext.Players.Remove(player);
            }
            _dbContext.GameBoards.Remove(gameboard);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoardId) == null)
                    throw new EntityNotFoundException("Nem található a gameboard");
                else throw;
            }
        }

        public async Task SetDiscardInDiscardingPhaseResultAsync(DiscardedGameBoardCard gameBoardCard, GameBoard gameBoard, CancellationToken cancellationToken)
        {
            if (gameBoard.TargetReason == TargetReason.Dynamite || gameBoard.TargetReason == TargetReason.DynamiteAndJail)
            {
                if (gameBoard.TargetReason == TargetReason.DynamiteAndJail)
                {
                    gameBoard.TargetReason = TargetReason.Jail;
                }
                else
                {
                    gameBoard.TargetReason = null;
                    gameBoard.TurnPhase = PhaseEnum.Drawing;
                }
                var dynamite = gameBoard.ActualPlayer.TablePlayerCards.FirstOrDefault(t => t.Card.CardType == CardType.Dynamite);
                if (gameBoardCard.CardColorType == CardColorType.Spades && gameBoardCard.FrenchNumber >= 2 && gameBoardCard.FrenchNumber <= 9)
                {
                    bool isOver = false;
                    for (int i = 0; i < 3 && !isOver; i++)
                    {
                        Player selectedPlayer = await _playerStore.GetOwnPlayerAsync(cancellationToken);
                        long newHP = await _playerStore.DecrementPlayerHealthAsync(cancellationToken);
                        if (selectedPlayer.CharacterType == CharacterType.BartCassidy)
                        {
                            await DrawGameBoardCardsFromTopAsync(1, selectedPlayer.Id, cancellationToken);
                        }
                        if (newHP == 0)
                        {
                            isOver = await CalculatePlayerPlacementAsync(selectedPlayer.Id, cancellationToken);
                            if (isOver)
                            {
                                await SetGameBoardEndAsync(cancellationToken);
                            }
                            else
                            {
                                await EndGameBoardTurnAsync(cancellationToken);
                            }
                        }
                    }
                    if (!isOver)
                    {
                        await _playerStore.DiscardCardAsync(dynamite.Id, cancellationToken);
                    }
                }
                else
                {
                    var nextPlayer = await _playerStore.GetNextPlayerAliveByPlayerAsync((long)gameBoard.ActualPlayerId, cancellationToken);
                    await _cardStore.PlacePlayerCardToAnotherTableAsync(dynamite, nextPlayer, cancellationToken);
                }
            }
            else if (gameBoard.TargetReason == TargetReason.Jail || gameBoard.TargetReason == TargetReason.JailAndDynamite)
            {
                if (gameBoardCard.CardColorType == CardColorType.Hearts)
                {
                    if (gameBoard.TargetReason == TargetReason.JailAndDynamite)
                    {
                        gameBoard.TargetReason = TargetReason.Dynamite;
                    }
                    else
                    {
                        gameBoard.TargetReason = null;
                        gameBoard.TurnPhase = PhaseEnum.Drawing;
                    }
                }
                else
                {
                    gameBoard.TargetReason = null;
                    gameBoard.TurnPhase = PhaseEnum.Discarding;
                }
                var jail = gameBoard.ActualPlayer.TablePlayerCards.FirstOrDefault(t => t.Card.CardType == CardType.Jail);
                await _cardStore.PlacePlayerCardToDiscardedAsync(jail, cancellationToken);
            }
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoards
                    .SingleOrDefaultAsync(g => g.Id == gameBoard.Id) == null)
                    throw new EntityNotFoundException("Nem található a gameboard");
                else if (await _dbContext.GameBoardCards
                    .SingleOrDefaultAsync(g => g.Id == gameBoardCard.Id) == null)
                    throw new EntityNotFoundException("Nem található a gameboardcard");
                else throw;
            }
        }

        public async Task SetDiscardInDiscardingPhaseResultAsync(IEnumerable<DiscardedGameBoardCard> gameBoardCards, GameBoard gameBoard, CancellationToken cancellationToken)
        {
            if (gameBoard.TargetReason == TargetReason.Dynamite || gameBoard.TargetReason == TargetReason.DynamiteAndJail)
            {
                if (gameBoard.TargetReason == TargetReason.DynamiteAndJail)
                {
                    gameBoard.TargetReason = TargetReason.Jail;
                }
                else
                {
                    gameBoard.TargetReason = null;
                    gameBoard.TurnPhase = PhaseEnum.Drawing;
                }
                bool goodResult = false;
                foreach (var gameBoardCard in gameBoardCards)
                {
                    if (!(gameBoardCard.CardColorType == CardColorType.Spades && gameBoardCard.FrenchNumber >= 2 && 
                          gameBoardCard.FrenchNumber <= 9))
                    {
                        goodResult = true;
                    }
                }
                if (!goodResult)
                {
                    bool isOver = false;
                    for (int i = 0; i < 3 && !isOver; i++)
                    {
                        Player selectedPlayer = await _playerStore.GetOwnPlayerAsync(cancellationToken);
                        long newHP = await _playerStore.DecrementPlayerHealthAsync(cancellationToken);
                        if (selectedPlayer.CharacterType == CharacterType.BartCassidy)
                        {
                            await DrawGameBoardCardsFromTopAsync(1, selectedPlayer.Id, cancellationToken);
                        }
                        if (newHP == 0)
                        {
                            isOver = await CalculatePlayerPlacementAsync(selectedPlayer.Id, cancellationToken);
                            if (isOver)
                            {
                                await SetGameBoardEndAsync(cancellationToken);
                            }
                            else
                            {
                                await EndGameBoardTurnAsync(cancellationToken);
                            }
                        }
                    }
                }
                else
                {
                    var nextPlayer = await _playerStore.GetNextPlayerAliveByPlayerAsync((long)gameBoard.ActualPlayerId, cancellationToken);
                    var dynamite = gameBoard.ActualPlayer.TablePlayerCards.FirstOrDefault(t => t.Card.CardType == CardType.Dynamite);
                    await _cardStore.PlacePlayerCardToAnotherTableAsync(dynamite, nextPlayer, cancellationToken);
                }
            }
            else if (gameBoard.TargetReason == TargetReason.Jail || gameBoard.TargetReason == TargetReason.JailAndDynamite)
            {
                bool goodResult = false;
                foreach (var gameBoardCard in gameBoardCards)
                {
                    if (gameBoardCard.CardColorType == CardColorType.Hearts)
                    {
                        goodResult = true;
                    }
                }
                if (goodResult)
                {
                    if (gameBoard.TargetReason == TargetReason.JailAndDynamite)
                    {
                        gameBoard.TargetReason = TargetReason.Dynamite;
                    }
                    else
                    {
                        gameBoard.TargetReason = null;
                        gameBoard.TurnPhase = PhaseEnum.Drawing;
                    }
                }
                else
                {
                    gameBoard.TargetReason = null;
                    gameBoard.TurnPhase = PhaseEnum.Discarding;
                }
                var jail = gameBoard.ActualPlayer.TablePlayerCards.FirstOrDefault(t => t.Card.CardType == CardType.Jail);
                await _cardStore.PlacePlayerCardToDiscardedAsync(jail, cancellationToken);
            }
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

        public async Task UseBarrelAsync(long playerId, CancellationToken cancellationToken)
        {
            var player = await _playerStore.GetPlayerAsync(playerId, cancellationToken);
            var gameboard = await GetGameBoardByUserAsync(player.UserId, cancellationToken);
            var discarded = await DiscardFromDrawableGameBoardCardAsync(cancellationToken);
            if (discarded.CardColorType == CardColorType.Hearts)
            {
                if (gameboard.TargetReason == TargetReason.Bang)
                {
                    await SetGameBoardTargetedPlayerAsync(null, cancellationToken);
                    await SetGameBoardTargetReasonAsync(null, cancellationToken);
                }
                else if (gameboard.TargetReason == TargetReason.Gatling)
                {
                    var next = await _playerStore.GetNextPlayerAliveByPlayerAsync(playerId, cancellationToken);
                    if (next.Id == gameboard.ActualPlayerId)
                    {
                        await SetGameBoardTargetedPlayerAsync(null, cancellationToken);
                        await SetGameBoardTargetReasonAsync(null, cancellationToken);
                    }
                    else
                    {
                        await SetGameBoardTargetedPlayerAsync(next.Id, cancellationToken);
                        await SetGameBoardTargetReasonAsync(TargetReason.Gatling, cancellationToken);
                    }
                }
            }
        }

        public async Task SetGameBoardLastTargetedPlayerAsync(long? lastTargetedPlayerId, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.LastTargetedPlayerId = lastTargetedPlayerId;
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
                    .SingleOrDefaultAsync(p => p.Id == lastTargetedPlayerId) == null)
                    throw new EntityNotFoundException("Nem található a player");
                else throw;
            }
        }

        public async Task SetGameBoardLobbyOwnerIdAsync(string lobbyOwnerId, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            GameBoard gameBoard = await GetGameBoardByUserAsync(userId, cancellationToken);
            gameBoard.LobbyOwnerId = lobbyOwnerId;
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
    }
}
