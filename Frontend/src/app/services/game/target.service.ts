import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Card, CardType, GameBoard, OtherPlayer, Permissions, Player, PlayerHighlightedType, ServiceDataTransfer, TargetPermission, TargetType } from 'src/app/models';
import { CardService } from './card.service';
import { PlayerService } from './player.service';

@Injectable({
  providedIn: 'root'
})
export class TargetService {
  targetPermission: TargetPermission | undefined;
  
  constructor(private playerService: PlayerService, private cardService: CardService) { }

  public playCardSelected(data: {type: TargetType | undefined, card: Card | undefined}, ownId: number): Observable<OtherPlayer[]> {
    if (data.type == TargetType.TargetCard) {
      this.targetPermission = {canTargetCards: true};
    } else if (data.type == TargetType.TargetPlayer) {
      this.targetPermission = {canTargetPlayers: true};
    } else if (data.type == TargetType.TargetPlayerOrCard) {
      this.targetPermission = {canTargetCards: true, canTargetPlayers: true}
    } else {
      this.targetPermission = {};
    }
    
    if (data.card?.cardType === CardType.Panic) {
      return this.setTargetables(1, ownId);
    } else if (data.card && this.cardService.getCanTargetEverywhere().includes(data.card.cardType)) {
      return this.setTargetables(6 /* Everyone */, ownId);
    } else {
      return this.setTargetables(undefined, ownId);
    }
  }

  public setTargetables(range: number | undefined, ownId: number): Observable<OtherPlayer[]> {
    if (range) {
      return this.playerService.getTargetablesByRange(ownId, range);
    }
    else {
      return this.playerService.getTargetables(ownId);
    }
  }

  public getCardTarget(transferData: ServiceDataTransfer, selectData: { id: number | undefined, isCard: boolean }) {
    if (transferData.ownboard && selectData.id) {
      transferData.ownboard.playCardFromTarget(selectData.id, selectData.isCard);
      this.targetPermission = undefined;
    }
  }

  public getTargetPlayerClass(transferData: ServiceDataTransfer, player: Player | OtherPlayer | undefined): PlayerHighlightedType {
    if (player?.id === transferData.gameboard?.targetedPlayerId) {
      return PlayerHighlightedType.Targeted;
    }
    else if (player?.id === transferData.gameboard?.actualPlayerId) {
      return PlayerHighlightedType.Actual;
    }
    return PlayerHighlightedType.None;
  }

  getTargetableType(ownPlayer: Player, player: OtherPlayer | undefined): TargetType {
    if (this.targetPermission && player) {
      if (ownPlayer.targetablePlayers.includes(player.id)) {
        if (this.targetPermission.canTargetCards && this.targetPermission.canTargetPlayers) {
          return TargetType.TargetPlayerOrCard;
        } else if (this.targetPermission.canTargetCards) {
          return TargetType.TargetCard;
        } else if (this.targetPermission.canTargetPlayers) {
          return TargetType.TargetPlayer;
        }
      }
    }
    return TargetType.None;
  }
}
