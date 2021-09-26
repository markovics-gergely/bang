import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoard } from 'src/app/models';
import { HttpClient } from '@angular/common/http';
import { GameboardService } from 'src/app/services/gameboard.service';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  gameboard: GameBoard | undefined;
  constructor(private gameBoardService: GameboardService, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.gameBoardService.getGameBoard(params['userid'])
        .subscribe(resp => this.gameboard = resp)
    }, () => {
      console.log("Could not get gameboard");
    })
  }


}
