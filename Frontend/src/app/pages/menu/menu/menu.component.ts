import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { stringify } from 'querystring';
import { RegistrationDto } from 'src/app/models';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { GameboardService } from 'src/app/services/game/gameboard.service';
import { TokenService } from 'src/app/services/authorization/token.service';
import { MenuService } from 'src/app/services/menu/menu.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { LobbyService } from 'src/app/services/menu/lobby.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  joinLobbyForm = new FormGroup({
    password: new FormControl('', Validators.required),
  });

  constructor(
    private tokenService: TokenService, 
    private authService: AuthorizationService, 
    private gameBoardService: GameboardService, 
    private snackbar: SnackbarService,
    private router: Router,
    private lobbyService: LobbyService
  ) {}

  ngOnInit(): void {
    this.lobbyService.getActualLobby().subscribe(
      response => {
        console.log(response);

        this.lobbyService.leaveLobby(response.id).subscribe(
          response2 => {
            console.log(response2);
          }
        );
      },
      error => {
        console.log(error);
      }
    );

    this.lobbyService.leaveLobby().subscribe(
      response => {
        console.log(response);
      }
    );
  }


  createLobby(){
    this.lobbyService.createLobby().subscribe(
      response => {
        console.log(response);

        this.snackbar.open("Lobby created!");
        this.router.navigate(['lobby']);
      },
      error => {
        console.log(error);

        this.snackbar.open(error.error.title);
      }
    );
  }

  joinLobby() {
    let password = this.joinLobbyForm.get('password')?.value;

    this.lobbyService.joinLobby(password).subscribe(
      response => {
        console.log(response);

        this.snackbar.open("Successful join!");
        this.router.navigate(['lobby']);
      },
      error => {
        console.log(error);

        this.snackbar.open(error.error.title);
      }
    );
  }

  viewHistory() {
    this.router.navigateByUrl('/history');
  }

  logout() {
    this.tokenService.deleteLocalStorage();
    this.router.navigateByUrl('/login');
  }

  deleteAccount() {

  }
}
