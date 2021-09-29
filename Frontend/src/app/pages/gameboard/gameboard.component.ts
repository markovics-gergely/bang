import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Card, GameBoard, OtherPlayer } from 'src/app/models';
import { HttpClient } from '@angular/common/http';
import { GameboardService, Position } from 'src/app/services/gameboard.service';
import { CardService } from 'src/app/services/card.service';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  gameboard: GameBoard | undefined;
  cardHovered: boolean = false;
  hoveredCard: Card | undefined;

  constructor(public gameBoardService: GameboardService, public cardService: CardService, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.gameBoardService.getGameBoard(params['userid'])
        .subscribe(resp => {this.gameboard = resp; console.log(resp);})
    }, () => {
      console.log("Could not get gameboard");
    })
  }

  public getPlayerByPosition(pos: Position) {
    return this.gameBoardService.getPlayerByPosition(pos, this.gameboard?.otherPlayers);
  }

  public setCardHovered(card: string) {
    if(card) {
      this.cardHovered = true;
      let obj = JSON.parse(card);
      this.hoveredCard = obj;
    } else {
      this.cardHovered = false;
      this.hoveredCard = undefined;
    }
  }
}