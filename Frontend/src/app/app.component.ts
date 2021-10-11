import { Component } from '@angular/core';
import { GameboardService } from './services/game/gameboard.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
const httpOptions = {
  headers: new HttpHeaders({ 
    'Access-Control-Allow-Origin':'*'
  })
};

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
   constructor(public gameboardService: GameboardService, private http: HttpClient) {}

   ngOnInit() {
    this.gameboardService.startConnection();
    this.gameboardService.addGetGameBoardListener();   
    this.startHttpRequest();
  }

  private startHttpRequest = () => {
    this.http.get('http://localhost:15300/character', httpOptions)
      .subscribe(res => {
      })
  }
}
