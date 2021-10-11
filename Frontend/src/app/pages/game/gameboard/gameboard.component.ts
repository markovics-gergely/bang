import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Card, CharacterType, GameBoard, HoverEnum, RoleType } from 'src/app/models';
import { HttpClient } from '@angular/common/http';
import { GameboardService, Position } from 'src/app/services/game/gameboard.service';
import { CardService } from 'src/app/services/game/card.service';
import { stringify } from 'querystring';
import { RoleService } from 'src/app/services/game/role.service';
import { CharacterService } from 'src/app/services/game/character.service';
import { environment } from 'src/environments/environment';
import { TokenService } from 'src/app/services/authorization/token.service';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  gameboard: GameBoard | undefined;
  hovered: boolean = false;
  hoveredCard: Card | undefined;
  hoveredRole: RoleType | undefined;
  hoveredCharacter: CharacterType | undefined;

  constructor(public gameBoardService: GameboardService, public cardService: CardService, public roleService: RoleService, public characterService: CharacterService,
              private route: ActivatedRoute, private http: HttpClient, private tokenService: TokenService) { }

  ngOnInit(): void {
    this.http.get<GameBoard[]>(`${environment.baseUrl}/api/bang/gameboard/all`).subscribe(resp => console.log(resp));
    this.http.get<Card[]>(`${environment.baseUrl}/api/bang/cards`).subscribe(resp => console.log(resp));

    this.gameBoardService.getGameBoard()
      .subscribe(resp => {this.gameboard = resp; console.log(resp);})
  }

  public getPlayerByPosition(pos: Position) {
    return this.gameBoardService.getPlayerByPosition(pos, this.gameboard?.otherPlayers);
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
    } else {
      this.hovered = false;
      this.hoveredCard = undefined;
      this.hoveredCharacter = undefined;
      this.hoveredRole = undefined;
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