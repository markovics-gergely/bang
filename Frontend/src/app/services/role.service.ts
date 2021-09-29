import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private static assetPath: string = "../../assets/cards/Roles/";

  getRolePath(role: string) {
    return RoleService.assetPath + role + ".png";
  }
}
