import { Injectable } from '@angular/core';
import { CardService } from '../card.service';
import { GameboardService } from '../gameboard.service';
import { PlayerService } from '../player.service';

@Injectable({
  providedIn: 'root'
})
export class JesseJonesService {

  constructor(private playerService: PlayerService, private cardService: CardService, private gameboardService: GameboardService) { }

  public drawCards(selectedPlayer: number | undefined) {
    if (selectedPlayer) {
      this.cardService.drawCardFromPlayer(selectedPlayer).subscribe(_ => {
        this.cardService.drawCards(1);
      });
    } else {
      this.cardService.drawCards(2);
    }
  }
}
