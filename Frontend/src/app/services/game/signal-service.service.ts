import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { GameboardComponent } from 'src/app/pages/game/gameboard/gameboard.component';
import { environment } from 'src/environments/environment';
import { AuthorizationService } from '../authorization/authorization.service';
import { GameboardService } from './gameboard.service';

@Injectable({
  providedIn: 'root'
})
export class SignalServiceService {

  constructor(private auth: AuthorizationService, private gameBoardService: GameboardService) { }

  public hubConnection: signalR.HubConnection | undefined;
  
  public startConnection(gameboardComponent: GameboardComponent) {
    this.auth.getActualUserId()
      .subscribe(id => {
        this.hubConnection = new signalR.HubConnectionBuilder()
          .withUrl(`${environment.bangBaseUrl}/game?userid=${id}`)
          .configureLogging(signalR.LogLevel.Information)
          .build();
        gameboardComponent.addrefreshListeners(this.hubConnection);
        this.hubConnection
          .start().then(() => {
            console.log('Game connection started');
            gameboardComponent.init();
          }).catch(err => console.log('Error while starting connection: ' + err));
      });                  
  }
}
