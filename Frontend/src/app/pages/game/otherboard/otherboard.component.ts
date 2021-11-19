import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, CharacterType, HoverEnum, OtherPlayer, Permissions, PlayerHighlightedType, RoleType, TargetPermission, TargetType } from 'src/app/models';
import { CardService } from 'src/app/services/game/card.service';
import { CharacterService } from 'src/app/services/game/character.service';
import { RoleService } from 'src/app/services/game/role.service';

@Component({
  selector: 'app-otherboard',
  templateUrl: './otherboard.component.html',
  styleUrls: ['./otherboard.component.css']
})
export class OtherboardComponent implements OnInit {
  @Input() player: OtherPlayer | undefined;
  @Input() permissions: Permissions | undefined;
  @Input() targetPermissions: TargetPermission | undefined;
  @Input() isTargetable: TargetType = TargetType.None;
  @Input() highlight: PlayerHighlightedType = PlayerHighlightedType.None;
  @Output() hoverItemEvent = new EventEmitter<{ data: string, type: HoverEnum }>();
  @Output() selectEvent = new EventEmitter<{ id: number | undefined, isCard: boolean }>();

  constructor(public roleService: RoleService, public characterService: CharacterService, public cardService: CardService) { }

  ngOnInit(): void {
  }

  counter(i: number) {
    return new Array(i);
  }

  getHighLightStyle(): string {
    switch(this.highlight) {
      case PlayerHighlightedType.Actual: return 'actual';
      case PlayerHighlightedType.Targeted: return 'targeted';
      case PlayerHighlightedType.None:
      default: return '';
    }
  }

  getTableClickable(): boolean {
    if(this.targetPermissions) {
      return this.targetPermissions.canTargetCards != undefined;
    }
    //TODO permissions
    return true;
  }

  getPlayerClickable(): boolean {
    if(this.targetPermissions) {
      return this.targetPermissions.canTargetPlayers != undefined;
    }
    //TODO permissions
    return true;
  }

  setCharacterHovered(e: MouseEvent, inside: boolean) {
    if (inside) {
      if (this.player) {
        let string = JSON.stringify(this.player.characterType);
        this.hoverItemEvent.emit({ data: string, type: HoverEnum.Character });
      }
    }
    else {
      this.hoverItemEvent.emit(undefined);
    }
  }

  setRoleHovered(e: MouseEvent, inside: boolean) {
    if (inside) {
        if (this.player && (this.player.actualHP == 0 || this.player?.roleType === RoleType.Sheriff)) {
          let string = JSON.stringify(this.player.roleType);
          this.hoverItemEvent.emit({ data: string, type: HoverEnum.Role });
        }
    }
    else {
      this.hoverItemEvent.emit(undefined);
    }
  }

  setCardHovered(card: string) {
    this.hoverItemEvent.emit({data: card, type: HoverEnum.Card});
  }

  playerSelected() {
    this.selectEvent.emit({ id: this.player?.id, isCard: false });
  }

  cardSelected(card: Card) {
    this.selectEvent.emit({ id: card.id, isCard: true });
  }

  playerTargetStyle(): string {
    if (this.isTargetable) {
      if (this.isTargetable === TargetType.TargetPlayer || this.isTargetable === TargetType.TargetPlayerOrCard) {
        return 'targetable';
      }
    }
    return '';
  }

  cardsTargetStyle(): string {
    if (this.isTargetable) {
      if (this.isTargetable === TargetType.TargetCard || this.isTargetable === TargetType.TargetPlayerOrCard) {
        return 'targetable';
      }
    }
    return '';
  }

  getNotWeapons(): Card[] {
    return this.player?.tablePlayerCards.filter(c => !this.cardService.isWeaponType(c.cardType)) || [];
  }

  getCardCountPath(): string {
    if (this.player) {
      return `../../../../assets/cards/Numbers/number_${this.player.handPlayerCardCount > 9 ? 10 : this.player.handPlayerCardCount}.png`
    }
    return "";
  }

  canSeeRole(): boolean {
    if (this.player) {
      return this.player.roleType === RoleType.Sheriff || this.player.actualHP == 0;
    }
    return false;
  }
}
