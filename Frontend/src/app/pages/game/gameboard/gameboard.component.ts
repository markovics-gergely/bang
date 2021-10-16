import { Component, OnInit, ViewChild } from '@angular/core';
import { Card, CharacterType, GameBoard, HoverEnum, Permissions, PlayCardDto, RoleType, TargetPermission } from 'src/app/models';
import { GameboardService, Position } from 'src/app/services/gameboard.service';
import { CardService, TargetType } from 'src/app/services/card.service';
import { RoleService } from 'src/app/services/role.service';
import { CharacterService } from 'src/app/services/character.service';
import { PlayerService } from 'src/app/services/player.service';
import { OwnboardComponent } from '../ownboard/ownboard.component';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  @ViewChild(OwnboardComponent)
  private ownBoard: OwnboardComponent | undefined;

  gameboard: GameBoard | undefined;
  permissions: Permissions | undefined;
  targetPermissions: TargetPermission | undefined;
  playTargetNeeded: TargetType | undefined;

  hovered: boolean = false;
  hoveredCard: Card | undefined;
  hoveredRole: RoleType | undefined;
  hoveredCharacter: CharacterType | undefined;

  constructor(public gameBoardService: GameboardService, public cardService: CardService, public roleService: RoleService, public characterService: CharacterService,
              public playerService: PlayerService) { }

  ngOnInit(): void {
    this.gameBoardService.getGameBoard()
      .subscribe(resp => {this.gameboard = resp});
    this.playerService.getPermissions()
      .subscribe(resp => this.permissions = resp);
  }

  public getPlayerByPosition(pos: Position) {
    return this.gameBoardService.getPlayerByPosition(pos, this.gameboard?.otherPlayers);
  }

  public discardFromDrawable() {
    this.gameBoardService.discardFromDrawable().subscribe(resp => console.log(resp));
  }

  public playCardSelected(target: TargetType) {
    this.playTargetNeeded = target;
    if (target == TargetType.TargetCard) {
      this.targetPermissions = {canTargetCards: true};
    } else if (target == TargetType.TargetPlayer) {
      this.targetPermissions = {canTargetPlayers: true};
    } else if (target == TargetType.TargetPlayerOrCard) {
      this.targetPermissions = {canTargetCards: true, canTargetPlayers: true}
    }
    console.log(this.ownBoard?.selectedCard);
  }

  public getCardTarget(selectData: { id: number | undefined, isCard: boolean }) {
    this.ownBoard?.playCardFromTarget(selectData.id as number, selectData.isCard);
  }

  public setCardHovered(hoverData: {data: string, type: HoverEnum}) {
    if(!hoverData) {
      this.hovered = false;
      this.hoveredCard = undefined;
      this.hoveredCharacter = undefined;
      this.hoveredRole = undefined;
    } else if(hoverData.type === HoverEnum.Card) {
      if(hoverData.data) {
        this.hovered = true;
        this.hoveredCard = JSON.parse(hoverData.data);
      } else {
        this.hovered = false;
        this.hoveredCard = undefined;
      }
    } else if(hoverData.type === HoverEnum.Character) {
      if(hoverData.data) {
        this.hovered = true;
        this.hoveredCharacter = JSON.parse(hoverData.data);
      } else {
        this.hovered = false;
        this.hoveredCard = undefined;
      }
    } else if(hoverData.type === HoverEnum.Role) {
      if(hoverData.data) {
        this.hovered = true;
        this.hoveredRole = JSON.parse(hoverData.data);
      } else {
        this.hovered = false;
        this.hoveredCard = undefined;
      }
    }
  }

  public setOnlyCardHovered(hoverData: string) {
    if(hoverData) {
      this.hovered = true;
      this.hoveredCard = JSON.parse(hoverData);
    } else {
      this.hovered = false;
      this.hoveredCard = undefined;
    }
  }

  getHoverType(val: Card | RoleType | CharacterType | undefined): string { return typeof val; }

  checkType(value: any) {
    return typeof value
  }
}