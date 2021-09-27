import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoard, OtherPlayer } from 'src/app/models';
import { HttpClient } from '@angular/common/http';
import { GameboardService } from 'src/app/services/gameboard.service';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  gameboard: GameBoard | undefined;
  constructor(private gameBoardService: GameboardService, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.gameBoardService.getGameBoard(params['userid'])
        .subscribe(resp => {this.gameboard = resp; console.log(resp);})
    }, () => {
      console.log("Could not get gameboard");
    })
  }

  public getPlayerByPosition(pos: Position) {
    let players = this.gameboard?.otherPlayers;
    if(players && this.gameboard) {
      let count = this.gameboard.otherPlayers.length;
      switch(pos) {
        case Position.LeftBottom : 
          return players[0];
        case Position.LeftTop :
          if(count < 6) {
            return undefined;
          } 
          return players[1];
        case Position.TopLeft :
          if(count < 6) {
            return players[1];
          }
          return players[2];
        case Position.TopRight :
          if(count <= 3) {
            return undefined;
          }
          if(count == 6) {
            return players[3];
          }
          return players[2];
        case Position.RightTop :
          if(count <= 4) {
            return undefined;
          }
          return players[count - 2];
        case Position.RightBottom : 
          return players[count - 1];
      }
    }
    return undefined;
  }

  public get Position(): typeof Position { return Position; }
}

export enum Position {
  LeftBottom,
  LeftTop,
  TopLeft,
  TopRight,
  RightTop,
  RightBottom
}