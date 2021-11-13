import { Injectable } from '@angular/core';
import { CardColorType } from 'src/app/models';
import { CardService } from '../card.service';
import { GameboardService } from '../gameboard.service';
import { PlayerService } from '../player.service';

@Injectable({
  providedIn: 'root'
})
export class BlackJackService {

  constructor(private playerService: PlayerService, private cardService: CardService, private gameboardService: GameboardService) { }

  public drawCard() {
    this.gameboardService.getCardsOnTop(2).subscribe(cards => {
      let second = cards[1];
      if (second.cardColorType === CardColorType.Hearts || second.cardColorType === CardColorType.Diamonds) {
        this.cardService.drawCards(3);
      } else {
        this.cardService.drawCards(2);
      }
    })
  }
}
