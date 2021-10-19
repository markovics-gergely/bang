﻿using Bang.DAL.Domain;
using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class GeneralStoreCardEffect : TargetPlayerCardEffect
    {
        public GeneralStoreCardEffect() : base(TargetReason.GeneralStore) { }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            await query.GameBoardStore.DrawGameBoardCardsToScatteredByPlayersAliveAsync(cancellationToken);
            var actual = query.PlayerCard.Player;
            var newQuery = new CardEffectQuery(query.PlayerCard, actual, query.GameBoardStore, query.CardStore, query.PlayerStore, query.AccountStore);
            await base.Execute(newQuery, cancellationToken);
        }
    }
}
