import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  @Input() fromBottom: number | undefined;
  @Input() animated: boolean = true;

  messages: String[] = [];

  constructor() { }

  ngOnInit(): void {
    this.messages.push("user1: asdasdadad");
    this.messages.push("user2: fsdfsdfsd");
    this.messages.push("user3: asdasvxcvxcvdadad");
  }

}
