import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account, Lobby, PostGameBoard, UserId } from 'src/app/models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LobbyService {
  constructor(private client: HttpClient) { }

  public getActualLobby(): Observable<Lobby> {
    return this.client.get<Lobby>(`${environment.baseUrl}/api/lobby/actual-lobby`);
  }

  public getLobbyUsers(id?: number): Observable<Account[]> {
    return this.client.get<Account[]>(`${environment.baseUrl}/api/lobby/${id}/users`);
  }
  
  public createLobby(): Observable<number> {
    return this.client.post<number>(`${environment.baseUrl}/api/lobby`, undefined);
  }

  public joinLobby(password: string): Observable<Object> {
    return this.client.post(`${environment.baseUrl}/api/lobby/connect/${password}`, undefined, { responseType: 'text' });
  }

  public leaveLobby(id?: number): Observable<Object>{
    return this.client.delete(`${environment.baseUrl}/api/lobby/${id}/disconnect`);
  }

  public sendInvite(friendName: string): Observable<Object>{
    return this.client.put(`${environment.baseUrl}/api/lobby/invite/${friendName}`, undefined);
  }

  public acceptInvite(friendName: string): Observable<Object>{
    return this.client.put(`${environment.baseUrl}/api/lobby/accept-invite/${friendName}`, undefined);
  }

  public startGame(gameBoardId: number, id?: number): Observable<Object>{
    return this.client.post(`${environment.baseUrl}/api/lobby/${id}/start-game?gameBoardId=${gameBoardId}`, undefined, { responseType: 'text' });
  }
}
