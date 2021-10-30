import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Account, Message } from '../models';

@Injectable({
  providedIn: 'root'
})
export class HubBuilderService {
  getConnection() {
    return new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    /*.withUrl("/lobbyhub")
    .withUrl("/friendhub")*/
    .configureLogging(signalR.LogLevel.Information)
    .build();
  }

  connection: signalR.HubConnection; 
  lobbyMessages: Message[];
  peeps: Account[];
  chatMessage: string | undefined;
  
  constructor(hubBuilder: HubBuilderService) {
    this.connection = hubBuilder.getConnection();

    this.connection.on("SetUsers", users => this.setUsers(users));
    this.connection.on("UserEntered", user => this.userEntered(user));
    this.connection.on("UserLeft", userId => this.userLeft(userId));
    this.connection.on("SetMessages", messages => this.setMessages(messages));

    this.peeps = [];
    this.lobbyMessages = [];
    this.connection.start().then(() => {
    this.connection.invoke("EnterLobby");
   });
  }

  ngOnInit() {}

  ngOnDestroy() {
   this.connection.off("SetUsers");
   this.connection.off("UserEntered");
   this.connection.off("UserLeft");
   this.connection.off("SetMessages");
   this.connection.stop(); 
  }
  userEntered(user: Account) {
   this.peeps.push(user);
  }
  userLeft(id: string) {
    //delete this.peeps[id];
  }
  setUsers(users: Account[]) {
   this.peeps = users;
  }
  setMessages(messages: Message[]) {
   this.lobbyMessages = messages;
  }
  recieveMessage(message: Message) {
    this.lobbyMessages.splice(0, 0, message);
   }
   sendMessage() {
    this.connection.invoke("SendMessageToLobby", this.chatMessage);
    this.chatMessage = "";
   }
  
}
