import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private client: HttpClient) { }

  public createLobby(): Observable<Object> {
    return this.client.post(`${environment.baseUrl}/api/lobby`, {responseType: 'text'});
  }
}
