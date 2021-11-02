import { Component, OnInit, Input } from '@angular/core';
import { Message } from 'src/app/models';
import { TokenService } from 'src/app/services/authorization/token.service';

@Component({
  selector: 'app-chat-stream',
  templateUrl: './chat-stream.component.html',
  styleUrls: ['./chat-stream.component.css']
})
export class ChatStreamComponent implements OnInit {
  @Input() messages: Message[] | undefined;
  public userName: string | undefined; 

  constructor(private tokenService: TokenService) { }

  ngOnInit() {
    this.userName = this.tokenService.getUsername();
  }

  isOwnMessage(message: Message) {
    return message.senderName === this.userName;
  }
}
