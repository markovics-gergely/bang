import { Component, ElementRef, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Card, CardActionType, CardType, CharacterType, GameBoard, HoverEnum, OtherPlayer, PermissionQueryType, Permissions, PlayCardDto, Player, PlayerHighlightedType, RoleType, ServiceDataTransfer, TargetPermission, TargetType } from 'src/app/models';
import { GameboardService, Position } from 'src/app/services/game/gameboard.service';
import { CardService } from 'src/app/services/game/card.service';
import { RoleService } from 'src/app/services/game/role.service';
import { CharacterService } from 'src/app/services/game/character.service';
import { PlayerService } from 'src/app/services/game/player.service';
import { OwnboardComponent } from '../ownboard/ownboard.component';
import { HoverService } from 'src/app/services/game/hover.service';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import * as signalR from '@microsoft/signalr';
import { CharacterManagerService } from 'src/app/services/game/characters/character-manager.service';
import { TargetService } from 'src/app/services/game/target.service';
import { PermissionService } from 'src/app/services/game/permission.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { SignalServiceService } from 'src/app/services/game/signal-service.service';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  @ViewChild(OwnboardComponent)
  ownBoard: OwnboardComponent | undefined;
  @ViewChild('content', {static: false}) private content: any;
  
  gameboard: GameBoard | undefined;
  permissions: Permissions | undefined;
  playTargetNeeded: TargetType | undefined;

  private ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false,
    centered : true,
    size : 'xl'
  };

  constructor(public gameBoardService: GameboardService, public cardService: CardService, private modalService: NgbModal,
              public roleService: RoleService, public characterService: CharacterService,
              public playerService: PlayerService, public hoverService: HoverService, private auth: AuthorizationService, 
              public characterManagerService: CharacterManagerService, public targetService: TargetService, public permissionService: PermissionService,
              private signalService: SignalServiceService) { }

  ngOnInit(): void {
    //this.gameBoardService.deleteGameBoard(2).subscribe(r => console.log(r));
    this.signalService.startConnection(this);
  }
  ngOnChanges(changes: SimpleChanges) {
    console.log(this.gameboard);
  }

  public addrefreshListeners(hubConnection: signalR.HubConnection | undefined) {
    hubConnection?.on('RefreshBoard', (data: GameBoard) => {
      this.gameboard = data;
      if (this.gameboard.isOver) {
        this.modalService.open(this.content, this.ngbModalOptions);
      }
    });
    hubConnection?.on('RefreshPermission', (data: Permissions) => {
      this.permissions = data;
    });
  }

  public init() {
    this.gameBoardService.getGameBoard()
      .subscribe(resp => {
        this.gameboard = resp; 
        if (this.gameboard.isOver) {
          this.modalService.open(this.content, this.ngbModalOptions);
        }
        this.playerService.getPermissions()
          .subscribe(perm => {this.permissions = perm; console.log(perm);});
      });
  }

  public getPlayerByPosition(pos: Position) {
    return this.gameBoardService.getPlayerByPosition(pos, this.gameboard?.otherPlayers);
  }

  public cardPackAction() {
    this.characterManagerService.cardPackAction(this);
  }

  public playCardSelected(data: {type: TargetType | undefined, card: Card | undefined}) {
    if (data.card && data.type && this.gameboard) {
      this.targetService.playCardSelected(data, this.gameboard.ownPlayer.id)
        .subscribe(others => {
          console.log(others);
          if (this.gameboard) {
            this.gameboard.ownPlayer.targetablePlayers = others.map(o => o.id)
          }
        });
    } else {
      this.targetPermission = {};
    }
  }

  public get targetPermission() {
    return this.targetService.targetPermission;
  }

  public set targetPermission(value: TargetPermission | undefined) {
    this.targetService.targetPermission = value;
  }

  public createServiceDataTransfer(): ServiceDataTransfer {
    let transfer: ServiceDataTransfer = {gameboard: this.gameboard, player: this.gameboard?.ownPlayer, 
                                         targetPermission: this.targetPermission, permissions: this.permissions, ownboard: this.ownBoard};
    return transfer;
  }
}