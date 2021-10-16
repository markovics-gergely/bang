import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, CharacterType, HoverEnum, OtherPlayer, Permissions, RoleType, TargetPermission } from 'src/app/models';
import { CardService } from 'src/app/services/card.service';
import { CharacterService } from 'src/app/services/character.service';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-otherboard',
  templateUrl: './otherboard.component.html',
  styleUrls: ['./otherboard.component.css']
})
export class OtherboardComponent implements OnInit {
  @Input() player: OtherPlayer | undefined;
  @Input() permissions: Permissions | undefined;
  @Input() targetPermissions: TargetPermission | undefined;
  @Output() hoverItemEvent = new EventEmitter<{ data: string, type: HoverEnum }>();
  @Output() selectEvent = new EventEmitter<{ id: number | undefined, isCard: boolean }>();

  constructor(public roleService: RoleService, public characterService: CharacterService, public cardService: CardService) { }

  ngOnInit(): void {
  }

  counter(i: number) {
    return new Array(i);
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
        if (this.player) {
          let string = JSON.stringify(this.player.roleType);
          this.hoverItemEvent.emit({ data: string, type: HoverEnum.Role });
        }
    }
    else {
      this.hoverItemEvent.emit(undefined);
    }
  }

  playerSelected() {
    this.selectEvent.emit({ id: this.player?.id, isCard: false });
  }

  cardSelected(card: Card) {
    this.selectEvent.emit({ id: card.id, isCard: true });
  }
}
