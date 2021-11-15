import { Injectable } from '@angular/core';
import { CardActionType, CardType, PermissionQueryType, ServiceDataTransfer } from 'src/app/models';
import { GameboardService } from './gameboard.service';
import { CardService } from './card.service';
import { RoleService } from './role.service';
import { CharacterService } from './character.service';
import { PlayerService } from './player.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor(public gameBoardService: GameboardService, public cardService: CardService, public roleService: RoleService, public characterService: CharacterService,
      public playerService: PlayerService) { }

  public get permissionQueryType(): typeof PermissionQueryType {
    return PermissionQueryType; 
  }

  public getPermissionByType(type: PermissionQueryType, transferData: ServiceDataTransfer): CardActionType {
    let canDo: boolean = false;
    switch (type) {
      case PermissionQueryType.CanSwitchPlayMode:
        canDo = this.canSwitchPlayMode(transferData);
        break;
      case PermissionQueryType.CanUseCardPack:
        canDo = this.canUseCardPack(transferData);
        break;
      case PermissionQueryType.CanUseDiscardedPack:
        canDo = this.canUseDiscardedPack(transferData);
        break;
      case PermissionQueryType.CanUseDiscardedPack:
        canDo = this.canUseDiscardedPack(transferData);
        break;
      case PermissionQueryType.CanUseBarrel:
        canDo = this.canUseBarrel(transferData);
        break;
      case PermissionQueryType.CanUseBang:
        canDo = this.canUseBang(transferData);
        break;
      case PermissionQueryType.CanUseMissed:
        canDo = this.canUseMissed(transferData);
        break;
      case PermissionQueryType.CanUseBeer:
        canDo = this.canUseBeer(transferData);
        break;
      case PermissionQueryType.CanEndTurn:
        canDo = this.canEndTurn(transferData);
        break;
      case PermissionQueryType.CanTargetOthers:
        canDo = this.canTargetOthers(transferData);
        break;
      case PermissionQueryType.CanTargetOthersHand:
        canDo = this.canTargetOthersHand(transferData);
        break;
      case PermissionQueryType.CanTargetOthersTable:
        canDo = this.canTargetOthersTable(transferData);
        break;
      case PermissionQueryType.CanDiscardCard:
        canDo = this.canUseDiscardedPack(transferData);
        break;
    }
    if (canDo) {
      return CardActionType.Play;
    }
    return CardActionType.None;
  }

  public getStyle(type: PermissionQueryType, transferData: ServiceDataTransfer): string {
    switch(this.getPermissionByType(type, transferData)) {
      case CardActionType.Discard: return 'discardable';
      case CardActionType.Play: return 'targetable';
      default: return '';
    }
  }

  public canPlayCardType(transferData: ServiceDataTransfer, cardType: CardType, playMode: boolean): CardActionType {
    if (!playMode && this.canDiscardCard(transferData)) {
      return CardActionType.Discard;
    }
    else if (playMode) {
      if (cardType === CardType.Bang) {
        return this.getPermissionByType(PermissionQueryType.CanUseBang, transferData);
      } else if (cardType === CardType.Missed) {
        return this.getPermissionByType(PermissionQueryType.CanUseMissed, transferData);
      } else if (cardType === CardType.Beer) {
        return this.getPermissionByType(PermissionQueryType.CanUseBeer, transferData);
      } else if (this.canPlayCard(transferData)){
        return CardActionType.Play;
      }
    }
    return CardActionType.None;
  }

  public canDiscardCard(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canDiscardCard;
    }
    return false;
  }

  public canUseCardPack(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canDrawCard || transferData.permissions.canDiscardFromDrawCard;
    }
    return false;
  }

  public canUseDiscardedPack(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canDrawFromDiscardCard;
    }
    return false;
  }

  public canPlayCard(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canPlayCard;
    }
    return false;
  }

  public canUseBarrel(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canUseBarrelCard;
    }
    return false;
  }

  public canUseMissed(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canPlayMissedCard;
    }
    return false;
  }

  public canUseBang(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canPlayBangCard;
    }
    return false;
  }

  public canUseBeer(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canPlayBeerCard;
    }
    return false;
  }

  public canEndTurn(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canEndTurn;
    }
    return false;
  }

  public canTargetOthers(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canTargetPlayers;
    } else if (transferData.targetPermission && transferData.targetPermission.canTargetPlayers) {
      return transferData.targetPermission.canTargetPlayers;
    }
    return false;
  }

  public canTargetOthersHand(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canDrawFromOthersHands;
    } else if (transferData.targetPermission && transferData.targetPermission.canTargetPlayers) {
      return transferData.targetPermission.canTargetPlayers;
    }
    return false;
  }

  public canTargetOthersTable(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canDrawFromOthersTable;
    } else if (transferData.targetPermission && transferData.targetPermission.canTargetCards) {
      return transferData.targetPermission.canTargetCards;
    }
    return false;
  }

  public canSwitchPlayMode(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      if (!transferData.permissions.canPlayCard) {
        return transferData.permissions.canDiscardCard;
      }
      return true;
    }
    return false;
  }

  public canLoseHealth(transferData: ServiceDataTransfer): boolean {
    if (transferData.targetPermission === undefined && transferData.permissions) {
      return transferData.permissions.canLoseHealth;
    }
    return false;
  }
}
