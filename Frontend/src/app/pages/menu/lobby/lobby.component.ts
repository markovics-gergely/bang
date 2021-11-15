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

    this.connection?.start().then(() => {
      this.connection?.invoke("EnterRoom");
    });
  }

  createGameBoard() {
    /*this.gameBoardService.postGameBoard({maxTurnTime: 5, lobbyOwnerId: '', userIds: [{userId: 'adc14af4-2de0-4ea1-90b3-4ee3714448d3', userName: 'abc'},
    {userId: '712fcfd7-ff83-475c-8d70-e0669fcb953c', userName: 'dummy1'},
    {userId: '31d1f037-9071-49cb-bafb-c6e8cd56f294', userName: 'dummy2'},
    {userId: '31bb9ea3-c3f2-4b1a-a7a7-ba63fa29209f', userName: 'dummy3'}]})
        .subscribe(r => {
          console.log(r);
          this.router.navigateByUrl('/gameboard');
        });*/
        
    let userIds: UserId[] = [];
    if (this.players && this.lobbyId) {
      this.players.forEach(element => {
        userIds.push({userId: element.id, userName: element.userName})
      });
  
      let owner = this.players.find(element => element.userName == this.tokenService.getUsername());
      if (owner) {
        this.lobbyService.startGame(owner.id, userIds, this.lobbyId);
      }
    }
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

  @HostListener("window:beforeunload")
  leaveLobbyPopup(){
    
  }

  leaveLobby(){
    this.connection?.invoke("LeaveLobby");

    this.lobbyService.leaveLobby(this.lobbyId).subscribe(
      response => {
        this.router.navigateByUrl('/menu');
      }
    );
  }

  ngOnDestroy(): void {
    this.connection?.off("RefreshLobbyUsers");  

    this.connection?.stop();
  }
}
