import { Injectable } from '@angular/core';
import { Card, CharacterType, GameBoard, HoverEnum, Permissions, PlayCardDto, RoleType, TargetPermission, TargetType } from 'src/app/models';
import { GameboardService, Position } from './gameboard.service';
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

  public cardPackAction(permission: Permissions) {
    if (permission.canDiscardFromDrawCard) {
      this.gameBoardService.discardFromDrawable();
    }
    else if (permission.canDrawCard) {
      this.cardService.drawCards(2).subscribe(resp => console.log(resp));
    }
  }
}
