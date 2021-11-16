import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, CardActionType, CardType, HoverEnum, Permissions, PlayCardDto, Player, PlayerHighlightedType, ServiceDataTransfer, TargetPermission, TargetType } from 'src/app/models';
import { CardService } from 'src/app/services/game/card.service';
import { CharacterService } from 'src/app/services/game/character.service';
import { PermissionService } from 'src/app/services/game/permission.service';
import { PlayerService } from 'src/app/services/game/player.service';
import { RoleService } from 'src/app/services/game/role.service';

@Component({
  selector: 'app-ownboard',
  templateUrl: './ownboard.component.html',
  styleUrls: ['./ownboard.component.css']
})
export class OwnboardComponent implements OnInit {
  @Input() player: Player | undefined;
  @Input() permissions: Permissions | undefined;
  @Input() targetPermissions: TargetPermission | undefined;
  @Input() hoverActive: boolean = false;
  @Input() highlight: PlayerHighlightedType = PlayerHighlightedType.None;
  @Output() hoverItemEvent = new EventEmitter<{data: string, type: HoverEnum}>();
  @Output() selectCardEvent = new EventEmitter<{type: TargetType | undefined, card: Card | undefined}>();
  @Output() invalidateEvent = new EventEmitter();

  public playMode: boolean = true;
  public canChangePlayMode: boolean = true;
  public selectedCard: Card | undefined;
  private targetPlayerId: number | undefined;
  private targetPlayerCardId: number | undefined;

  constructor(public cardService: CardService, public characterService: CharacterService, public roleService: RoleService,
              public playerService: PlayerService, public permissionService: PermissionService) { }

  ngOnInit(): void {}

  public cardHovered(card: string) {
    this.hoverItemEvent.emit({data: card, type: HoverEnum.Card});
  }

  public cardAction(card: Card) {
    if (this.permissionService.canPlayCardType(this.createServiceDataTransfer(), card.cardType, this.playMode) !== CardActionType.None) {
      this.invalidateEvent.emit();
      if (this.playMode) {
        this.playCard(card);
      } else {
        this.discardCard(card);
        this.canChangePlayMode = false;
      }
    } else if (card === this.selectedCard) {
      this.selectedCard = undefined;
      this.targetPermissions = undefined;
      this.selectCardEvent.emit({type: undefined, card: undefined});
    }
  }

  public discardCard(card: Card) {
    this.playerService.discardFromHand(card.id).subscribe(resp => {console.log(resp); this.targetPermissions = undefined}, _ => this.targetPermissions = undefined);
  }

  public playCard(card: Card) {
    let target: TargetType = this.cardService.needsTargetPlayerOrCard(card.cardType, this.highlight);
    if (target == TargetType.None) {
      let playCardDto: PlayCardDto = {playerCardId: card.id, targetPlayerCardId: this.targetPlayerCardId, targetPlayerId: this.targetPlayerId}; 
      this.targetPermissions = {};
      this.playerService.playFromHand(playCardDto).subscribe(resp => {console.log(resp); this.targetPermissions = undefined});
    }
    else {
      this.selectedCard = card;
      this.selectCardEvent.emit({type: target, card: card});
    }
  }

  public playCardFromTarget(id: number, isCard: boolean) {
    let playCardDto;
    if (isCard) {
      playCardDto = {playerCardId: this.selectedCard?.id as number, targetPlayerCardId: id};
    } else {
      playCardDto = {playerCardId: this.selectedCard?.id as number, targetPlayerId: id}; 
    }
    this.targetPermissions = {};
    this.playerService.playFromHand(playCardDto).subscribe(resp => {console.log(resp); this.targetPermissions = undefined}, _ => this.targetPermissions = undefined);
  }

  public endTurn() {
    this.playerService.endTurn().subscribe(resp => {console.log(resp); this.targetPermissions = undefined}, _ => this.targetPermissions = undefined);
  }

  counter(i: number) {
    return new Array(i);
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
        if (this.player) {
          let string = JSON.stringify(this.player.roleType);
          this.hoverItemEvent.emit({ data: string, type: HoverEnum.Role });
        }
    }
    else {
      this.hoverItemEvent.emit(undefined);
    }
  }

  decrementHealth() {
    this.playerService.decrementPlayerHealth().subscribe(resp => console.log(resp));
  }

  switchPlayMode() {
    if (this.canChangePlayMode) {
      this.playMode = !this.playMode;
    }
  }

  canPlayCard(card: Card): CardActionType {
    return this.permissionService.canPlayCardType(this.createServiceDataTransfer(), card.cardType, this.playMode)
  }

  canEndTurn(): boolean {
    if (!this.targetPermissions && this.permissions) {
      return this.permissions.canEndTurn;
    }
    return false;
  }

  getHighLightStyle(): string {
    switch(this.highlight) {
      case PlayerHighlightedType.Actual: return 'actual';
      case PlayerHighlightedType.Targeted: return 'targeted';
      case PlayerHighlightedType.None:
      default: return '';
    }
  }

  public createServiceDataTransfer(): ServiceDataTransfer {
    let transfer: ServiceDataTransfer = {player: this.player, targetPermission: this.targetPermissions, permissions: this.permissions, ownboard: this};
    return transfer;
  }
}
