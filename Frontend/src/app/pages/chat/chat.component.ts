import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgScrollbar } from 'ngx-scrollbar';
import { Scrollbar } from 'ngx-scrollbar/lib/scrollbar/scrollbar';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  @Input() fromBottom: number | undefined;
  @Input() animated: boolean = true;
  @ViewChild('scrollable')
  scrollable: NgScrollbar | undefined; 
  messageValue: string = "";
  messages: string[] = [];
  hovered: boolean | undefined;
  height: number = 50;

  setHovered(value: boolean) {
    if (this.animated) {
      this.hovered = value;
      this.height = this.hovered ? 250 : 50;
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 800}), 500);
    }
  }

  constructor() { }

  ngOnInit(): void {
    this.hovered = !this.animated;
    this.height = this.hovered ? 250 : 50;
    this.scrollable?.scrollTo({bottom: 0, duration: 800});
    this.messages.push("user1: asdasdadad");
    this.messages.push("user2: fsdfsdfsd");
    this.messages.push("user3: asdasvxcvxcvdadad");
    this.messages.push("user3: asdasv xcvx cvdadadasdss ssss ssss sssss sss ssssssssssssss ssssssssss sss ssssss ssssssss ssssssss sssssss ss sss");
    this.messages.push("user3: asd asvx cvxcv dadadasds sssssssss ssssss ssss sssssssss ssssss sss sssssss ssssssss ssssssss ssssssssss ssssss ss");
    this.messages.push("user3: asdasvxcvxcvdadad");
    this.messages.push("user3: asda svxcvxcvda dadasd ssssss sssss ssssssss sssss ssssssss ssssss ssssssss sss ssss sssssss ssssssssssss sssssss");
    this.messages.push("user3: asdasvxcvxcvdadad"); 
    this.messages.push("user3: asdasvx cvxcvd adadasdssss ssss ssssss sssss sss sssssssss ssssss sssssss ss sssssssss sssss ssssss ssssss sssssss");
  }

  sendMessage() {
    if (this.messageValue) {
      this.messages.push(this.messageValue);
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 200}), 200);
      this.messageValue = "";
    }
  }

}
