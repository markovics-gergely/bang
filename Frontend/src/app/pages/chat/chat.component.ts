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
  }

  sendMessage() {
    if (this.messageValue) {
      this.messages.push(this.messageValue);
      setTimeout(() => this.scrollable?.scrollTo({bottom: 0, duration: 200}), 200);
      this.messageValue = "";
    }
  }

}
