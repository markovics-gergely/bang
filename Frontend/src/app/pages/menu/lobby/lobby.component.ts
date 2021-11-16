import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { Account, LoginDto, RegistrationDto, UserId } from 'src/app/models';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { TokenService } from 'src/app/services/authorization/token.service';
import { GameboardService } from 'src/app/services/game/gameboard.service';
import { LobbyService } from 'src/app/services/menu/lobby.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit, OnDestroy {
  private lobbyId: number | undefined;
  public lobbyPassword: string | undefined;
  public players: Account[] | undefined;
  public isLobbyOwner: boolean | undefined;
  public isStarted: boolean = false;

  private connection: signalR.HubConnection | undefined;

  constructor(   
    private authService: AuthorizationService, 
    private tokenService: TokenService,  
    private gameBoardService: GameboardService, 
    private snackbar: SnackbarService,
    private router: Router,
    private lobbyService: LobbyService
  ) { }

  ngOnInit(): void {
    this.lobbyService.getActualLobby().subscribe(
      response => {
        this.lobbyId = response.id;
        this.lobbyPassword = response.password;

        if(response.lobbyOwner == this.tokenService.getUsername()){
          this.isLobbyOwner = true;
        }
        else{
          this.isLobbyOwner = false;
        }

        this.refreshLobbyUsers(response.id);
      },
      error => {
        console.log(error);
      }
    ); 

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.userIdentityBaseUrl}/lobbyhub?token=${this.tokenService.getAccessToken()}`) 
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection?.on("RefreshLobbyUsers", lobbyId => this.refreshLobbyUsers(lobbyId));  
    this.connection?.on("NavigateToGameboard", _ => this.navigateToGameboard());  

    this.connection?.start().then(() => {
      this.connection?.invoke("EnterLobby");
    });
  }

  createGameBoard() { 
    this.isStarted = true;
    let userIds: UserId[] = [];
    if (this.players && this.lobbyId) {
      this.players.forEach(element => {
        userIds.push({userId: element.id, userName: element.userName})
      });
  
      let owner = this.players.find(element => element.userName == this.tokenService.getUsername());
      if (owner) {    
        this.gameBoardService.postGameBoard({maxTurnTime: 5, lobbyOwnerId: owner.id, userIds: userIds}).subscribe(
          response => {
            this.lobbyService.startGame(response, this.lobbyId).subscribe(
              response2 => {
                this.connection?.invoke("NavigateToGameboard");
              }
            );
          }
        );
      }
    }
  }

  isStartable(): boolean { 
    if (this.players && !this.isStarted) { 
      if(this.players?.length > 3 && this.players?.length < 8) {
        return true;
      }
    }
    return false;
  }

  navigateToGameboard() {
    this.router.navigateByUrl('/gameboard');
  }

  refreshLobbyUsers(lobbyId: number) {
    this.lobbyService.getLobbyUsers(lobbyId).subscribe(
      response => {
        this.players = response;
      },
      error => {
        console.log(error);
      }
    );
  }

  leaveLobby(){
    this.lobbyService.leaveLobby(this.lobbyId).subscribe(
      response => {   
        this.connection?.invoke("LeaveLobby", this.lobbyPassword, this.lobbyId);
        this.router.navigateByUrl('/menu');
      }
    );
  }

  ngOnDestroy(): void {
    this.connection?.off("RefreshLobbyUsers");  

    this.connection?.stop();
  }
}
