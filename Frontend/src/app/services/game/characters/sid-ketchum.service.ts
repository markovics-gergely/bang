import { Injectable } from '@angular/core';
import { Card } from 'src/app/models';
import { CardService } from '../card.service';
import { PlayerService } from '../player.service';

@Injectable({
  providedIn: 'root'
})
export class SidKetchumService {
  private cardsSelected: number[] = [];
  private cardsNeeded: number = 2;

  constructor(private playerService: PlayerService) {}

  public addCard(cardId: number) {
    if (!this.cardsSelected.includes(cardId)) {
      this.cardsSelected.push(cardId);
      if (this.cardsSelected.length == this.cardsNeeded) {
        this.playerService.gainHealthForCards(this.cardsSelected).subscribe(() => this.cardsSelected = []);
      }
    }
  }

  public removeCard(cardId: number) {
    this.cardsSelected = this.cardsSelected.filter(c => c != cardId);
  }
}
