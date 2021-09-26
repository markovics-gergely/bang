import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { GameBoard } from '../models';

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

  public getGameBoard(userId: String) {
    return this.client.get<GameBoard>("http://localhost:15300/GameBoard/user/" + userId)
  }
  constructor(private client: HttpClient) { }
}
