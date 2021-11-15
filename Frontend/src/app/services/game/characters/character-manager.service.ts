import { Injectable } from '@angular/core';
import { Card, CharacterType, GameBoard, Permissions, Player, ServiceDataTransfer, TargetPermission } from 'src/app/models';
import { GameboardComponent } from 'src/app/pages/game/gameboard/gameboard.component';
import { CardService } from '../card.service';
import { GameboardService } from '../gameboard.service';
import { BlackJackService } from './black-jack.service';
import { JesseJonesService } from './jesse-jones.service';
import { KitCarlsonService } from './kit-carlson.service';
import { SidKetchumService } from './sid-ketchum.service';

@Injectable({
  providedIn: 'root'
})
export class CharacterManagerService {
  player: Player | undefined;
  gameboard: GameBoard | undefined;
  targetPermission: TargetPermission | undefined;
  permissions: Permissions | undefined;
  private gameboardComponent: GameboardComponent | undefined;

  private specialDrawingType: CharacterType[] = [CharacterType.JesseJones, CharacterType.KitCarlson];

  constructor(private sidKetchumService: SidKetchumService, private jesseJonesService: JesseJonesService, private blackJackService: BlackJackService,
              private kitCarlsonService: KitCarlsonService, private cardService: CardService, private gameBoardService: GameboardService) { }

  public update(gameboardComponent: GameboardComponent) {
    this.gameboardComponent = gameboardComponent;
    this.targetPermission = gameboardComponent.targetPermission;
    this.permissions = gameboardComponent.permissions;
    this.gameboard = gameboardComponent.gameboard;
    this.player = gameboardComponent.gameboard?.ownPlayer;
  }

  public cardPackAction(transferData: ServiceDataTransfer) {
    if(transferData.permissions?.canDiscardFromDrawCard) {
      this.gameBoardService.discardFromDrawable().subscribe(resp => console.log(resp));
    }
    else if (transferData.permissions?.canDrawCard) {
      this.cardService.drawCards(2).subscribe(resp => console.log(resp));
    }
  }

  public characterAction() {
    if (this.player?.characterType === CharacterType.SidKetchum) {

    }
  }

  private drawCard() {
    if (this.specialDrawingType.includes(this.player!.characterType)) {

    } else {
      this.drawCardRegular();
    }
  }

  private drawCardRegular() {
    if (this.player && this.player.characterType === CharacterType.BlackJack) {
      this.blackJackService.drawCard();
    } else {
      this.cardService.drawCards(2);
    }
  }

  private drawCardWithPlayer(selectedPlayer: number | undefined) {
    if (this.player?.characterType === CharacterType.JesseJones) {
      this.jesseJonesService.drawCards(selectedPlayer);
    }
  }

  private scatterCardsTemp() {
    if (this.player?.characterType === CharacterType.KitCarlson) {
      this.gameBoardService.getCardsOnTop(3).subscribe(cards => {
        if (this.gameboard) {
          this.gameboard.scatteredGameBoardCards = cards;
        }
      });
    }
  }

  public cardSelected(cardId: number) {
    if (this.player?.characterType === CharacterType.KitCarlson) {
      this.kitCarlsonService.addCard(cardId);
    } else if (this.player?.characterType === CharacterType.SidKetchum) {
      this.sidKetchumService.addCard(cardId);
    }
  }

  public cardRemoved(cardId: number) {
    if (this.player?.characterType === CharacterType.KitCarlson) {
      this.kitCarlsonService.removeCard(cardId);
    } else if (this.player?.characterType === CharacterType.SidKetchum) {
      this.sidKetchumService.removeCard(cardId);
    }
  }
}
