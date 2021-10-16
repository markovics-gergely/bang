import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, HoverEnum, PlayCardDto, Player } from 'src/app/models';
import { CardService, TargetType } from 'src/app/services/card.service';
import { CharacterService } from 'src/app/services/character.service';
import { PlayerService } from 'src/app/services/player.service';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-ownboard',
  templateUrl: './ownboard.component.html',
  styleUrls: ['./ownboard.component.css']
})
export class OwnboardComponent implements OnInit {
  @Input() player: Player | undefined;
  @Input() hoverActive: boolean = false;
  @Output() hoverItemEvent = new EventEmitter<{data: string, type: HoverEnum}>();
  @Output() selectCardEvent = new EventEmitter<TargetType>();

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
    if (this.playMode) {
      this.playCard(card);
    } else {
      this.discardCard(card);
      this.canChangePlayMode = false;
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
      this.selectCardEvent.emit(target);
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
}
