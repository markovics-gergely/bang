import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoard } from 'src/app/models';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-gameboard',
  templateUrl: './gameboard.component.html',
  styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {
  gameboard: GameBoard | undefined;
  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    /*this.route.params.subscribe(params => {
      this.http.get<GameBoard>('gameboard/user/' + params['userid'])
        .subscribe(resp => this.gameboard = resp)
    })*/
  }

}
