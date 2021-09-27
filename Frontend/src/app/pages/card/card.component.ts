import { Component, Input, OnInit } from '@angular/core';
import { Card, CardColorType, CardType } from 'src/app/models';
import { CardColorTypePipe } from 'src/app/pipes/card-color-type.pipe';
import { CardNumberPipe } from 'src/app/pipes/card-number.pipe';
import { CardTypePipe } from 'src/app/pipes/card-type.pipe';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  @Input() card: Card | undefined;

  private static assetPath: string = "../../../assets/cards/Cards/";

  constructor() { }

  ngOnInit(): void {
  }

  public getCardBack() {
    if(this.card){
      return CardComponent.assetPath + (this.card.cardType as CardType | CardTypePipe);
    }
    return null;
  }

  public getCardColorFilter() {
    if(this.card){
      return CardComponent.assetPath + "colors/" + (this.card.cardColorType as CardColorType | CardColorTypePipe);
    }
    return null;
  }

  public getCardNumberFilter() {
    if(this.card){
      return CardComponent.assetPath + "numbers/" + 
                            (this.isRed(this.card.cardColorType) ? "red/" : "black/") + 
                            (this.card.frenchNumber as number | CardNumberPipe);
    }
    return null;
  }

  public isRed(cardColorType: CardColorType) {
    if(cardColorType == CardColorType.Hearts || cardColorType == CardColorType.Diamonds) {
      return true;
    }
    return false;
  }
}