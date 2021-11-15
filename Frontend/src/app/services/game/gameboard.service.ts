import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Card, GameBoard, OtherPlayer, Player, PostGameBoard } from '../../models';
import { AuthorizationService } from '../authorization/authorization.service';

@Injectable({
  providedIn: 'root'
})
export class GameboardService {
  
  public getGameBoard(): Observable<GameBoard> {
    return this.client.get<GameBoard>(`${environment.baseUrl}/api/bang/gameboard/user`);
  }

  public getCardsOnTop(count: number): Observable<Card[]> {
    return this.client.get<Card[]>(`${environment.baseUrl}/api/bang/gameboard/cards-on-top/${count}`);
  }

  public postGameBoard(userIds: PostGameBoard): Observable<number> {
    return this.client.post<number>(`${environment.baseUrl}/api/bang/gameboard`, userIds);
  }

  public discardFromDrawable(): Observable<Card> {
    return this.client.post<Card>(`${environment.baseUrl}/api/bang/gameboard/discard-card-from-drawable`, undefined);
  }

  public endTurn(): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/gameboard/end-turn`, undefined);
  }

  public deleteGameBoard(id: number) {
    return this.client.delete(`${environment.baseUrl}/api/bang/gameboard/${id}`);
  }

  constructor(private client: HttpClient, private auth: AuthorizationService) { }

  public getPlayerByPosition(pos: Position, players: OtherPlayer[] | undefined): OtherPlayer | undefined {
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