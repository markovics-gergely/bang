import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { stringify } from 'querystring';
import { RegistrationDto } from 'src/app/models';
import { AuthorizationService } from 'src/app/services/authorization.service';
import { GameboardService } from 'src/app/services/gameboard.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor(private tokenService: TokenService, private authService: AuthorizationService, private gameBoardService: GameboardService, private router: Router) { }

  ngOnInit(): void {
  }

  createGameBoard() {
    var users: RegistrationDto[] = [{username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}, 
                                    {username: "user1", password: "@Abc1", confirmedPassword: "@Abc1"}];
    users.forEach(async u => this.authService.registration(u));
    var userData: {userName: string, userId: string}[] = [];
    users.forEach(u => userData.push({userName: u.username, userId: "1"}));
    this.authService.getActualUserId().subscribe(resp => {
      console.log(userData);
      userData.push({userName: this.tokenService.getUsername(), userId: resp});
      
      this.gameBoardService.postGameBoard({maxTurnTime: 5, userIds: userData})
        .subscribe(r => {
          console.log(r);
          this.router.navigateByUrl('/gameboard');
        });
    });
    
    
  }
}
