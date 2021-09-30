import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card, HoverEnum, Player } from 'src/app/models';

@Component({
  selector: 'app-ownboard',
  templateUrl: './ownboard.component.html',
  styleUrls: ['./ownboard.component.css']
})
export class OwnboardComponent implements OnInit {
  @Input() player: Player | undefined;
  @Output() hoverItemEvent = new EventEmitter<{data: string, type: HoverEnum}>();

  private playMode: boolean = true;

  constructor() { }

  ngOnInit(): void {
    console.log(this.player);
  }

  public cardHovered(card: string) {
    this.hoverItemEvent.emit({data: card, type: HoverEnum.Card});
  }
}
