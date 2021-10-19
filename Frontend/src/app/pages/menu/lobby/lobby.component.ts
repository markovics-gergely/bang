import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Account } from 'src/app/models';
import { LobbyService } from 'src/app/services/menu/lobby.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit, OnDestroy {
  private lobbyId: number | undefined;
  private players: Account[] | undefined;

  constructor(    
    private snackbar: SnackbarService,
    private router: Router,
    private lobbyService: LobbyService
  ) {}

  ngOnInit(): void {
    this.lobbyService.getActualLobbyId().subscribe(
      response => {
        console.log(response);

        this.lobbyId = response;
        this.lobbyService.getLobbyUsers(response).subscribe(
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

  ngOnDestroy(): void {
    this.lobbyService.leaveLobby(this.lobbyId)
  }
}
