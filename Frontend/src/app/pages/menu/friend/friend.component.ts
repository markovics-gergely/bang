import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { Account, Friend, Room } from 'src/app/models';
import { TokenService } from 'src/app/services/authorization/token.service';
import { FriendService } from 'src/app/services/menu/friend.service';
import { LobbyService } from 'src/app/services/menu/lobby.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements OnInit, OnDestroy {
  @Input() inLobby: boolean | undefined;
  public friends: Friend[] | undefined;
  public unacceptedFriends: Friend[] | undefined;

  friend: Friend | undefined;

  friendAddForm = new FormGroup({
    name: new FormControl('', Validators.required),
  });

  private connection: signalR.HubConnection | undefined;

  ngOnInit() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.userIdentityBaseUrl}/friendhub?token=${this.tokenService.getAccessToken()}`) 
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection?.on("RefreshFriendList", _ => this.refreshFriendList());

    this.connection.start();

    this.getFriendList();
  }

  ngOnDestroy() {
    this.connection?.off("RefreshFriendList");

    this.connection?.stop();
  }

  constructor(
    private friendService: FriendService,
    private snackbar: SnackbarService,
    private lobbyService: LobbyService,
    private tokenService: TokenService,
    private router: Router
  ) {}

  refreshFriendList(){
    this.getFriendList();
  }
  
  getFriendList(){
    this.friendService.getAcceptedFriends().subscribe(
      response => {
        console.log(response);

        this.friends = response;
      },
      error => {
        console.log(error);
      }
    );

    this.friendService.getUnacceptedFriends().subscribe(
      response => {
        console.log(response);

        this.unacceptedFriends = response;
      },
      error => {
        console.log(error);
      }
    );
  }

  removeFriend(friendName: string){
    this.friendService.removeFriend(friendName).subscribe(
      response => {
        console.log(response);

        this.snackbar.open("Removed succesfully!");
        this.connection?.invoke("RemoveFriend", friendName);
        this.getFriendList();
      },
      error => {
        console.log(error);

        this.snackbar.open(error.error.title);
      }
    );
  }

  addFriend(){
    let friendName = this.friendAddForm.get('name')?.value;

    this.friendService.addFriend(friendName).subscribe(
      response => {
        console.log(response);

        this.snackbar.open("Added succesfully!");
        this.connection?.invoke("AddFriend", friendName);
      },
      error => {
        console.log(error);

        this.snackbar.open(error.error.title);
      }
    );

    this.friendAddForm.reset();
  }

  addFriendFromList(friendName: string){
    this.friendService.addFriend(friendName).subscribe(
      response => {
        console.log(response);

        this.connection?.invoke("AcceptFriendRequest", friendName);
        this.getFriendList();
      },
      error => {
        console.log(error);
      }
    );
  }

  sendInvite(friendName: string){
    this.lobbyService.sendInvite(friendName).subscribe(
      response => {
        console.log(response);

        this.snackbar.open("Invited successfully!");
        this.connection?.invoke("InviteFriend", friendName);
      },
      error => {
        console.log(error);
      }
    );
  }

  acceptInvite(friendName: string){
    this.lobbyService.acceptInvite(friendName).subscribe(
      response => {
        console.log(response);

        this.router.navigate(['lobby']);
      },
      error => {
        console.log(error);

        this.snackbar.open(error.error.title);
      }
    );
  }
}
