import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Card, CardColorType, CardType, TargetType } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  private static assetPath: string = "../../assets/cards/";

  constructor(private client: HttpClient) { }
  
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

  public drawCards(count: number): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/card/draw-card/${count}`, undefined)
  }

  public drawCardById(id: number): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/card/draw-a-card/${id}`, undefined)
  }

  public drawCardFromPlayer(playerId: number): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/card/draw-a-card-from-hand/${playerId}`, undefined)
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

  public isWeaponType(cardType: CardType): boolean {
    switch(cardType) {
      case CardType.Karabine:
      case CardType.Remingtion:
      case CardType.Schofield:
      case CardType.Volcanic:
        return true;
      default: return false;
    }
  }

  public needsTargetPlayerOrCard(cardType: CardType): TargetType {
    switch(cardType) {
      case CardType.Bang:
      case CardType.Duel:
      case CardType.Jail: return TargetType.TargetPlayer;
      case CardType.Panic:
      case CardType.CatBalou: return TargetType.TargetPlayerOrCard;
      default: return TargetType.None;
    }
  }

  public getCanTargetEverywhere(): CardType[] {
    return [CardType.CatBalou, CardType.Duel];
  }
}