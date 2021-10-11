import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Role, RoleType } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private static assetPath: string = "../../../assets/cards/Roles/";

  constructor(private client: HttpClient) {
  }

  getRolePath(role: string): string {
    return RoleService.assetPath + role + ".png";
  }

  getRoleByType(type: RoleType): Observable<Role> {
    return this.client.get<Role>(`${environment.bangBaseUrl}role/` + type);
  }
}
