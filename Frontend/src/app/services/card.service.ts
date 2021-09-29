import { Injectable } from '@angular/core';
import { Card, CardColorType, CardType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  private static assetPath: string = "../../assets/cards/";

  public getCardBack(type: string) {
    return CardService.assetPath + "Cards/" + type + ".png";
  }

  public getCardColorFilter(color: string) {
    return CardService.assetPath + "French/colors/" + color + ".png";
  }

  public getCardNumberFilter(number: string, cardColorType: CardColorType) {
    return CardService.assetPath + "French/numbers/" + 
                            (this.isRed(cardColorType) ? "red/" : "black/") + number + ".png";
  }

  public isRed(cardColorType: CardColorType) {
    if(cardColorType == CardColorType.Hearts || cardColorType == CardColorType.Diamonds) {
      return true;
    }
    return false;
  }

  public isActive(cardType: CardType) {
    return cardType.valueOf() < 12;
  }

  public getWeapon(cards: Card[]) {
    let weapon = undefined;
    cards.forEach(c => {
      if(this.isWeaponType(c.cardType)) {
        weapon = c;
      }
    });
    return weapon;
  }

  public isWeaponType(cardType: CardType) {
    switch(cardType) {
      case CardType.Karabine:
      case CardType.Remingtion:
      case CardType.Schofield:
      case CardType.Volcanic:
        return true;
      default: return false;
    }
  }
}