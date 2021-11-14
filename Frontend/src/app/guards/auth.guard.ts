import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthorizationService } from "../services/authorization/authorization.service";
  
@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
      private authService: AuthorizationService,
      private router: Router
  ) { }

  canActivate(): boolean | Promise<boolean> {
    var isAuthenticated = this.authService.isAuthenticated();

    if (!isAuthenticated) {
      this.router.navigate(['/login']);
    }
    else{
      this.authService.getActualUserStatus().subscribe(
        response => {
          if(response.lobbyId == null && response.gameBoardId == null){
            this.router.navigate(['/menu']);
          }
          else if(response.lobbyId != null && response.gameBoardId == null){
            this.router.navigate(['/lobby']);
          }
          else if(response.lobbyId != null && response.gameBoardId != null){
            this.router.navigate(['/gameboard']);
          }
        }
      );
    }

    return isAuthenticated;
  }
}
