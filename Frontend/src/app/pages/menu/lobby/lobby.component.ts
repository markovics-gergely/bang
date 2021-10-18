import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Account } from 'src/app/models';
import { MenuService } from 'src/app/services/menu/menu.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit, OnDestroy {
  private lobbyId: number = -1;
  private players: Account[] = [];

  constructor(    
    private snackbar: SnackbarService,
    private router: Router,
    private menuService: MenuService
  ) {}

  ngOnInit(): void {
    this.menuService.getActualLobbyId().subscribe(
      response => {
        console.log(response);

        this.lobbyId = response;
      },
      error => {
        console.log(error);
      }
    );

    this.menuService.getLobbyUsers(this.lobbyId).subscribe(
      response => {
        console.log(response);

        this.players = response;
      },
      error => {
        console.log(error);
      }
    );
  }

  ngOnDestroy(): void {
    this.menuService.leaveLobby(this.lobbyId)
  }
}
