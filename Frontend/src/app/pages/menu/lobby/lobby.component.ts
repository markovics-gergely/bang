import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Account, RegistrationDto } from 'src/app/models';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { TokenService } from 'src/app/services/authorization/token.service';
import { GameboardService } from 'src/app/services/game/gameboard.service';
import { LobbyService } from 'src/app/services/menu/lobby.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

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

  constructor(   
    private authService: AuthorizationService, 
    private tokenService: TokenService,  
    private gameBoardService: GameboardService, 
    private snackbar: SnackbarService,
    private router: Router,
    private lobbyService: LobbyService
  ) {}

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

        this.lobbyService.getLobbyUsers(response.id).subscribe(
          response2 => {
            console.log(response2);
    
            this.players = response2;
          },
          error2 => {
            console.log(error2);
          }
        );
      },
      error => {
        console.log(error);
      }
    );
  }

  createGameBoard() {
    var users: RegistrationDto[] = [{username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}];
    users.forEach(async u => this.authService.registration(u));
    var userData: {userName: string, userId: string}[] = [];
    users.forEach(u => userData.push({userName: u.username, userId: "1"}));
    this.authService.getActualUserId().subscribe(resp => {
      console.log(userData);
      userData.push({userName: this.tokenService.getUsername(), userId: resp});
      
      this.gameBoardService.postGameBoard({maxTurnTime: 5, userIds: userData})
        .subscribe(r => {
          console.log(r);
          this.router.navigateByUrl('/gameboard');
        });
    });   
  }

  leaveLobby(){
    this.lobbyService.leaveLobby(this.lobbyId).subscribe(
      response => {
        console.log(response);
        this.router.navigateByUrl('/menu');
      }
    );
  }

  ngOnDestroy(): void {
    //this.lobbyService.leaveLobby(this.lobbyId);
  }
}
