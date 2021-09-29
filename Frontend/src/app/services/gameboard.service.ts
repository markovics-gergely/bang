import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { GameBoard, OtherPlayer } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GameboardService {
  public gameboard: GameBoard | undefined;
  
  private hubConnection: signalR.HubConnection | undefined;
  
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:15300/game')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Game connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))                        
  }

  public addGetGameBoardListener = () => {
    this.hubConnection?.on('getGameBoard', (data) => {
      this.gameboard = data;
      console.log(data);
    })
  }

  public getGameBoard(userId: string) {
    return this.client.get<GameBoard>("http://localhost:15300/GameBoard/user/" + userId)
  }

  public getFilledOthers(otherPlayers: OtherPlayer[]) {
    
  }

  constructor(private client: HttpClient) { }

  public getPlayerByPosition(pos: Position, players: OtherPlayer[] | undefined) {
    if(players) {
      let count = players.length;
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