import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, CardActionType, CardType, HoverEnum, Permissions, PlayCardDto, Player, PlayerHighlightedType, TargetPermission, TargetType } from 'src/app/models';
import { CardService } from 'src/app/services/game/card.service';
import { CharacterService } from 'src/app/services/game/character.service';
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
  @Output() selectCardEvent = new EventEmitter<{type: TargetType, card: Card}>();

  public playMode: boolean = true;
  public canChangePlayMode: boolean = true;
  public selectedCard: Card | undefined;
  private targetPlayerId: number | undefined;
  private targetPlayerCardId: number | undefined;

  constructor(public cardService: CardService, public characterService: CharacterService, public roleService: RoleService,
              public playerService: PlayerService) { }

  ngOnInit(): void {
  }

  public cardHovered(card: string) {
    this.hoverItemEvent.emit({data: card, type: HoverEnum.Card});
  }

  public cardAction(card: Card) {
    if (this.cardCanPlay(card) !== CardActionType.None) {
      if (this.playMode) {
        this.playCard(card);
      } else {
        this.discardCard(card);
        this.canChangePlayMode = false;
      }
    } else if (card === this.selectedCard) {
      this.selectedCard = undefined;
      this.targetPermissions = undefined;
    }
  }

  public discardCard(card: Card) {
    this.playerService.discardFromHand(card.id).subscribe(resp => console.log(resp));
  }

  public playCard(card: Card) {
    let target: TargetType = this.cardService.needsTargetPlayerOrCard(card.cardType);
    if (target == TargetType.None) {
      let playCardDto: PlayCardDto = {playerCardId: card.id, targetPlayerCardId: this.targetPlayerCardId, targetPlayerId: this.targetPlayerId}; 
      this.playerService.playFromHand(playCardDto).subscribe(resp => console.log(resp));
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
    this.playerService.playFromHand(playCardDto).subscribe(resp => console.log(resp));
  }

  public endTurn() {
    this.playerService.endTurn().subscribe(resp => console.log(resp));
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

  cardCanPlay(card: Card): CardActionType {
    if (this.selectedCard) {
      return CardActionType.None;
    }
    else if (!this.playMode && this.permissions) {
      return this.permissions.canDiscardCard ? CardActionType.Discard : CardActionType.None;
    }
    else if (this.playMode && this.permissions && this.permissions.canPlayCard) {
      if (card.cardType === CardType.Bang) {
        return this.permissions.canPlayBangCard ? CardActionType.Play : CardActionType.None;
      }
      else if (card.cardType === CardType.Missed) {
        return this.permissions.canPlayMissedCard ? CardActionType.Play : CardActionType.None;
      }
      else if (card.cardType === CardType.Beer) {
        return this.permissions.canPlayBeerCard ? CardActionType.Play : CardActionType.None;
      }
      else {
        return CardActionType.Play;
      }
    }
    return CardActionType.None;
  }

  canEndTurn(): boolean {
    if (!this.targetPermissions && this.permissions) {
      return this.permissions.canEndTurn;
    }
    return false;
  }

  canSwitchPlayMode(): boolean {
    if (this.permissions) {
      if (!this.permissions.canPlayCard) {
        return this.permissions.canDiscardCard;
      }
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
}
