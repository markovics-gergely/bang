import { Component, OnInit, ViewChild } from '@angular/core';
import { Card, CardType, CharacterType, GameBoard, HoverEnum, OtherPlayer, Permissions, PlayCardDto, Player, PlayerHighlightedType, RoleType, TargetPermission, TargetType } from 'src/app/models';
import { GameboardService, Position } from 'src/app/services/game/gameboard.service';
import { CardService } from 'src/app/services/game/card.service';
import { RoleService } from 'src/app/services/game/role.service';
import { CharacterService } from 'src/app/services/game/character.service';
import { PlayerService } from 'src/app/services/game/player.service';
import { OwnboardComponent } from '../ownboard/ownboard.component';
import { HoverService } from 'src/app/services/game/hover.service';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  @ViewChild(OwnboardComponent)
  private ownBoard: OwnboardComponent | undefined;

  gameboard: GameBoard | undefined;
  permissions: Permissions | undefined;
  targetPermissions: TargetPermission | undefined;
  playTargetNeeded: TargetType | undefined;

  constructor(public gameBoardService: GameboardService, public cardService: CardService, public roleService: RoleService, public characterService: CharacterService,
              public playerService: PlayerService, public hoverService: HoverService, private auth: AuthorizationService) { }

  ngOnInit(): void {
    this.startConnection();

    this.gameBoardService.getGameBoard()
      .subscribe(resp => {
        this.gameboard = resp; 
        this.playerService.getPermissions()
          .subscribe(perm => {this.permissions = perm; console.log(perm)});
      });
  }

  private hubConnection: signalR.HubConnection | undefined;
  
  public startConnection = () => {
    this.auth.getActualUserId()
      .subscribe(id => {
        this.hubConnection = new signalR.HubConnectionBuilder()
          .withUrl(`${environment.bangBaseUrl}/game?userid=${id}`)
          .configureLogging(signalR.LogLevel.Information)
          .build();
        this.addrefreshListeners();
        this.hubConnection
          .start().then(() => console.log('Game connection started'))
          .catch(err => console.log('Error while starting connection: ' + err));
      });                  
  }

  public addrefreshListeners = () => {
    this.hubConnection?.on('RefreshBoard', (data: GameBoard) => {
      this.gameboard = data;
    });
    this.hubConnection?.on('RefreshPermission', (data: Permissions) => {
      this.permissions = data;
    });
  }

  public getPlayerByPosition(pos: Position) {
    return this.gameBoardService.getPlayerByPosition(pos, this.gameboard?.otherPlayers);
  }

  public cardPackAction() {
    if(this.permissions?.canDiscardFromDrawCard) {
      this.gameBoardService.discardFromDrawable();
    }
    else if (this.permissions?.canDrawCard) {
      this.cardService.drawCards(2).subscribe(resp => console.log(resp));
    }
  }

  public endTurnAction() {
    if (this.targetPermissions === undefined) {
      this.playerService.endTurn();
    } 
  }

  public playCardSelected(data: {type: TargetType, card: Card}) {
    this.playTargetNeeded = data.type;
    if (data.card.cardType === CardType.Panic) {
      this.setTargetables(1);
    } else if (this.cardService.getCanTargetEverywhere().includes(data.card.cardType)) {
      this.setTargetables(6 /* Everyone */);
    } else {
      this.setTargetables(undefined);
    }
    if (data.type == TargetType.TargetCard) {
      this.targetPermissions = {canTargetCards: true};
    } else if (data.type == TargetType.TargetPlayer) {
      this.targetPermissions = {canTargetPlayers: true};
    } else if (data.type == TargetType.TargetPlayerOrCard) {
      this.targetPermissions = {canTargetCards: true, canTargetPlayers: true}
    }
  }

  private setTargetables(range: number | undefined) {
    if (range && this.gameboard) {
      this.playerService.getTargetablesByRange(this.gameboard.ownPlayer.id, range)
        .subscribe(resp => {
          let others: OtherPlayer[] = resp;
          if (this.gameboard) {
            this.gameboard.ownPlayer.targetablePlayers = others.map(o => o.id);
          }
        });
    }
    else if (this.gameboard) {
      this.playerService.getTargetables(this.gameboard.ownPlayer.id)
        .subscribe(resp => {
          let others: OtherPlayer[] = resp;
          if (this.gameboard) {
            this.gameboard.ownPlayer.targetablePlayers = others.map(o => o.id);
          }
        });
    }
  }

  public getCardTarget(selectData: { id: number | undefined, isCard: boolean }) {
    if (this.ownBoard && selectData.id) {
      this.ownBoard.playCardFromTarget(selectData.id, selectData.isCard);
      this.targetPermissions = undefined;
    }
  }

  public setCardHovered(hoverData: {data: string, type: HoverEnum}) {
    this.hoverService.setCardHovered(hoverData);
  }

  public setOnlyCardHovered(hoverData: string) {
    this.hoverService.setOnlyCardHovered(hoverData);
  }

  public getTargetPlayerClass(player: Player | OtherPlayer | undefined): PlayerHighlightedType {
    if (player?.id === this.gameboard?.actualPlayerId) {
      return PlayerHighlightedType.Actual;
    }
    else if (player?.id === this.gameboard?.targetedPlayerId) {
      return PlayerHighlightedType.Targeted;
    }
    return PlayerHighlightedType.None;
  }

  getTargetableType(player: OtherPlayer | undefined): TargetType {
    if (this.targetPermissions && this.gameboard && player) {
      if (this.gameboard.ownPlayer.targetablePlayers.includes(player.id)) {
        if (this.targetPermissions.canTargetCards && this.targetPermissions.canTargetPlayers) {
          return TargetType.TargetPlayerOrCard;
        } else if (this.targetPermissions.canTargetCards) {
          return TargetType.TargetCard;
        } else if (this.targetPermissions.canTargetPlayers) {
          return TargetType.TargetPlayer;
        }
      }
    }
    return TargetType.None;
  }
}