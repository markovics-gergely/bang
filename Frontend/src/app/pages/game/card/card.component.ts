import { style } from '@angular/animations';
import { Component, EventEmitter, HostBinding, Input, OnInit, Output } from '@angular/core';
import { JsonHubProtocol } from '@microsoft/signalr';
import { Card, CardColorType, CardType } from 'src/app/models';
import { CardColorTypePipe } from 'src/app/pipes/card-color-type.pipe';
import { CardNumberPipe } from 'src/app/pipes/card-number.pipe';
import { CardTypePipe } from 'src/app/pipes/card-type.pipe';
import { CardService } from 'src/app/services/card.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  host: {
    '[style.height.px]':'height',
    '[style.width.px]':'width',
    '[style.--glow-color]':'glowColor'
  }
})
export class CardComponent implements OnInit {
  @Input() card: Card | undefined;
  @Output() cardHoverEvent = new EventEmitter<string>();

  @Input() public width: number | undefined;
  @Input() public height: number | undefined;
  @Input() public glowColor: string | undefined;
  
  constructor(private cardService: CardService) { }

  ngOnInit(): void {
    
  }

  ngOnChanges() {
    console.log(this.glowColor);
  }

  public getCardBack(type: string) {
    if(this.card){
      return this.cardService.getCardBack(type);
    }
    return null;
  }

  public getCardColorFilter(color: string) {
    if(this.card){
      return this.cardService.getCardColorFilter(color);
    }
    return null;
  }

  public getCardNumberFilter(number: string) {
    if(this.card){
      return this.cardService.getCardNumberFilter(number, this.card.cardColorType);
    }
    return null;
  }

  public setCovered(inside: boolean) {
    if(inside) {
      let string = JSON.stringify(this.card);
      this.cardHoverEvent.emit(string);
    } else {
      this.cardHoverEvent.emit(undefined);
    }
    
  }
}
