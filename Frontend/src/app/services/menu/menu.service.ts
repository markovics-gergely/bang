import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private client: HttpClient) { }

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded'
    }),
    responseType: 'text'
  };
  
  public createLobby(): Observable<string> {
    return this.client.post(`${environment.baseUrl}/api/lobby`, undefined, { responseType: 'text' });
  }
}
