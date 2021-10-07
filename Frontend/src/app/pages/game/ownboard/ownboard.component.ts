import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, HoverEnum, Player } from 'src/app/models';
import { CardService } from 'src/app/services/card.service';
import { CharacterService } from 'src/app/services/character.service';
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

  playMode: boolean = true;

  constructor(public cardService: CardService, public characterService: CharacterService, public roleService: RoleService) { }

  ngOnInit(): void {
  }

  public cardHovered(card: string) {
    this.hoverItemEvent.emit({data: card, type: HoverEnum.Card});
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

  onHover() {
  }
}
