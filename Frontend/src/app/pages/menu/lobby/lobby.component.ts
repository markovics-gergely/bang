import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { Account, LoginDto, RegistrationDto } from 'src/app/models';
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
    /*this.createDummyUsers().subscribe(userDatas => {
      this.gameBoardService.postGameBoard({maxTurnTime: 5, userIds: userDatas})
        .subscribe(r => {
          console.log(r);
          this.router.navigateByUrl('/gameboard');
        });
    });*/

    this.gameBoardService.postGameBoard({maxTurnTime: 5, lobbyPassword: this.lobbyPassword || '', userIds: [{userId: 'b84bfcf3-ca16-4513-9601-8f6e7fbba019', userName: 'abc'},
    {userId: 'fc7b5de2-cb67-47f2-a30e-2b323c68fa26', userName: 'dummy1'},
    {userId: 'bcb1e873-9cf5-4305-87b2-b29925c81483', userName: 'dummy2'},
    {userId: '0b0e0632-4223-49c3-9759-f105c3a3ebad', userName: 'dummy3'}]})
        .subscribe(r => {
          console.log(r);
          this.router.navigateByUrl('/gameboard');
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
