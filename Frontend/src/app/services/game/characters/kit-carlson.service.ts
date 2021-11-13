import { Injectable } from '@angular/core';
import { CardService } from '../card.service';

@Injectable({
  providedIn: 'root'
})
export class KitCarlsonService {
  private cardsSelected: number[] = [];
  private cardsNeeded: number = 2;

  constructor(private cardService: CardService) {}

  public addCard(cardId: number) {
    if (!this.cardsSelected.includes(cardId)) {
      this.cardsSelected.push(cardId);
      if (this.cardsSelected.length == this.cardsNeeded) {
        this.drawCards();
        this.cardsSelected = [];
      }
    }
  }

  public removeCard(cardId: number) {
    this.cardsSelected = this.cardsSelected.filter(c => c != cardId);
  }

  private drawCards() {
    this.cardsSelected.forEach(cardId => {
      this.cardService.drawCardById(cardId);
    })
  }
}
