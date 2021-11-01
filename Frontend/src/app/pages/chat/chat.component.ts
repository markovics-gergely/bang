import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgScrollbar } from 'ngx-scrollbar';
import { Scrollbar } from 'ngx-scrollbar/lib/scrollbar/scrollbar';
import { Account, Message, Room } from 'src/app/models';
import { HubBuilderService } from 'src/app/services/hub-builder.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {
  @Input() fromBottom: number | undefined;
  @Input() animated: boolean = true;
  @ViewChild('scrollable')
  scrollable: NgScrollbar | undefined; 
  messageValue: string = "";
  messages: string[] = [];
  hovered: boolean | undefined;
  height: number = 50;

  activeTab: 'rooms' | 'peeps' = 'peeps';

  rooms: Room[];
  peeps: Account[];

  newRoomName: string | undefined;
  newRoomIsPrivate: boolean = false;
  newRoomPasskey: string | undefined;

  lobbyMessages: Message[];
  lobbyLoading: boolean = false;

  chatMessage: string | undefined;

  connection: signalR.HubConnection; 

  setHovered(value: boolean) {
    if (this.animated) {
      this.hovered = value;
      this.height = this.hovered ? 250 : 50;
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 800}), 500);
    }
  }

  ngOnInit(): void {
    this.hovered = !this.animated;
    this.height = this.hovered ? 250 : 50;
    this.scrollable?.scrollTo({bottom: 0, duration: 800});
  }

  constructor(hubBuilder: HubBuilderService, private router: Router) {
   this.connection = hubBuilder.getConnection();

   this.connection.on("SetUsers", users => this.setUsers(users));
   this.connection.on("UserEntered", user => this.userEntered(user));
   this.connection.on("UserLeft", userId => this.userLeft(userId));
   this.connection.on("SetMessages", messages => this.setMessages(messages));
   this.connection.on("SetRooms", rooms => this.setRooms(rooms));
   this.connection.on("RecieveMessage", message => this.recieveMessage(message));
   this.connection.on("JoinRoom", room => this.enterRoom(room));
   this.connection.on("RoomCreated", room => this.roomCreated(room));
   this.connection.on("RoomAbandoned", room => this.roomAbandoned(room));
   this.peeps = [];
   this.lobbyMessages = [];
   this.rooms = [];
   this.connection.start().then(() => {
   this.connection.invoke("EnterLobby");
   });
  }

  ngOnDestroy() {
    this.connection.off("SetUsers");
    this.connection.off("UserEntered");
    this.connection.off("UserLeft");
    this.connection.off("SetMessages");
    this.connection.off("SetRooms");
    this.connection.off("RecieveMessage");
    this.connection.off("JoinRoom");
    this.connection.off("RoomCreated");
    this.connection.off("RoomAbandoned");
    this.connection.stop(); 
   }
   userEntered(user: Account) {
    this.peeps.push(user);
   }
   userLeft(userId: string) {
    // a szerver azt jelezte, hogy a megadott ID-jú felhasználó elhagyta a szobát, így ki kell
    // vennünk a felhasználót a felhasználók tömbjéből ID alapján. 
   this.peeps = this.peeps.filter((peep) => peep.name != userId);
   }
   setUsers(users: Account[]) {
    // A szerver belépés után leküldi nekünk a teljes user listát:
    this.peeps = users;
   }
   setMessages(messages: Message[]) {
    // A szerver belépés után leküldi nekünk a korábban érkezett üzeneteket:
    this.lobbyMessages = messages;
   }
   
   setRooms(rooms: Room[]) {
    // A szerver belépés után leküldi nekünk a korábban érkezett üzeneteket:
    this.rooms = rooms;
   }

   joinRoom(roomId: string) {
      this.router.navigate(['room', roomId])
   }

   recieveMessage(message: Message) {
    this.lobbyMessages.splice(0, 0, message);
   }
   
   sendMessage() {
    if (this.messageValue) {
      this.messages.push(this.messageValue);
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 200}), 200);
      this.messageValue = "";
    }

    this.connection.invoke("SendMessageToLobby", this.chatMessage);
    this.chatMessage = "";
   }

  createRoom() {
    this.connection.invoke("CreateRoom", this.newRoomName);
    this.newRoomName = '';
  }

  roomCreated(room: Room) {
    this.rooms.push(room);
  }

  roomAbandoned(roomName: string) {
    this.rooms = this.rooms.filter((room) => room.name != roomName)
  }

  enterRoom(room: Room) {
    this.router.navigate(['room', `${room.name}`]);
  }

}
