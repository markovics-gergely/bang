import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgScrollbar } from 'ngx-scrollbar';
import { Account, Message, Room } from 'src/app/models';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { TokenService } from 'src/app/services/authorization/token.service';

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
  hovered: boolean | undefined;
  height: number = 50;

  message: Message | undefined;
  messages: Message[] = [];

  private connection: signalR.HubConnection | undefined;

  ngOnInit(){
    this.hovered = !this.animated;
    this.height = this.hovered ? 250 : 50;
    this.scrollable?.scrollTo({bottom: 0, duration: 800});

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.userIdentityBaseUrl}/chathub?token=${this.tokenService.getAccessToken()}`) 
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection?.on("SetMessages", messages => this.setMessages(messages));
    this.connection?.on("SetMessage", message => this.setMessage(message));  

    this.connection?.start().then(() => {
      this.connection?.invoke("EnterLobby");
    });
  }

  ngOnDestroy() {
    this.connection?.off("SetMessages");
    this.connection?.off("SetMessage");

    this.connection?.stop();
  }

  constructor(
    private router: Router, 
    private tokenService: TokenService
  ) {}

  setMessages(messages: Message[]) {
    this.messages = messages;
  }

  setMessage(message: Message) {
    this.messages.push(message);
    setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 200}), 200);
    /*this.animated = true;
    this.setHovered(false);
    setTimeout(() => {this.setHovered(true); this.animated = false}, 2000);*/
  }
  
  sendMessage() {
    if (this.message) {
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 200}), 200);
      this.connection?.invoke("SendMessageToLobby", this.message);
      this.message = undefined;
    }
  }

  setHovered(value: boolean) {
    if (this.animated) {
      this.hovered = value;
      this.height = this.hovered ? 250 : 50;
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 800}), 500);
    }
  }
}
