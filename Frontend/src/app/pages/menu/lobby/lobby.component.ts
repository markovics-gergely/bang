import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { Account, LoginDto, RegistrationDto } from 'src/app/models';
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
        console.log(response);

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

  registerAndGet(username: string, password: string): Observable<{userName: string, userId: string}> {
    var subject = new Subject<{userName: string, userId: string}>();
    let req: RegistrationDto = {username: username, password: password, confirmedPassword: password};
    this.tokenService.deleteLocalStorage();
    this.authService.registration(req).subscribe(_ => {
      console.log(_);
      let log: LoginDto = {username: username, password: password};
      this.authService.login(log).subscribe(__ => {
        console.log(__);
        this.authService.getActualUserId().subscribe(id => subject.next({userId: id, userName: username}));
      })
    }, _ => {
      console.log(_);
      let log: LoginDto = {username: username, password: password};
      this.authService.login(log).subscribe(__ => {
        console.log(__);
        this.authService.getActualUserId().subscribe(id => subject.next({userId: id, userName: username}));
      })
    });
    return subject.asObservable();
  }

  createDummyUsers(): Observable<{userName: string, userId: string}[]> {
    let newUsers: {userName: string, userId: string}[] = [];
    var subject = new Subject<{userName: string, userId: string}[]>();
    var actualUser = this.tokenService.getUsername();
    this.registerAndGet("Dummy1", "@Asd123").subscribe(user1 => {
      newUsers.push(user1);
      this.registerAndGet("Dummy2", "@Asd123").subscribe(user2 => {
        newUsers.push(user2);
        this.registerAndGet("Dummy3", "@Asd123").subscribe(user3 => {
          newUsers.push(user3);
          this.registerAndGet(actualUser, "@Asd123").subscribe(act => {
            newUsers.push(act);
            subject.next(newUsers);
          });
        });
      });
    });
    return subject.asObservable();
  }

  createGameBoard() {
      
  }

  refreshLobbyUsers(lobbyId: number) {
    this.lobbyService.getLobbyUsers(lobbyId).subscribe(
      response => {
        console.log(response);

        this.players = response;
      },
      error => {
        console.log(error);
      }
    );
  }

  leaveLobby(){
    this.connection?.invoke("LeaveLobby");
    this.router.navigateByUrl('/menu');
  }

  ngOnDestroy(): void {
    this.connection?.off("RefreshLobbyUsers");  

    this.connection?.stop();
  }
}
