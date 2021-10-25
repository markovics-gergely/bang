import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Friend } from 'src/app/models';
import { FriendService } from 'src/app/services/menu/friend.service';
import { LobbyService } from 'src/app/services/menu/lobby.service';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements OnInit {
  @Input() inLobby: boolean | undefined;
  public friends: Friend[] | undefined;
  public unacceptedFriends: Friend[] | undefined;

  friendAddForm = new FormGroup({
    name: new FormControl('', Validators.required),
  });

  constructor(
    private friendService: FriendService,
    private lobbyService: LobbyService
  ) {}

  ngOnInit(): void {
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
        this.getFriendList();
      },
      error => {
        console.log(error);
      }
    );
  }

  addFriend(){
    let friendName = this.friendAddForm.get('name')?.value;

    this.friendService.addFriend(friendName).subscribe(
      response => {
        console.log(response);
        this.getFriendList();
      },
      error => {
        console.log(error);
      }
    );

    this.friendAddForm.reset();
  }

  addFriendFromList(friendName: string){
    this.friendService.addFriend(friendName).subscribe(
      response => {
        console.log(response);
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
      },
      error => {
        console.log(error);
      }
    );
  }
}
