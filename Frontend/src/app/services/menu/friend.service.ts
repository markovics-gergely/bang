import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Friend } from 'src/app/models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendService {
  constructor(private client: HttpClient) { }

  getAcceptedFriends(): Observable<Friend[]>{
    return this.client.get<Friend[]>(`${environment.baseUrl}/api/friends`);
  }

  getUnacceptedFriends(): Observable<Friend[]>{
    return this.client.get<Friend[]>(`${environment.baseUrl}/api/unaccepted-friends`);
  }

  addFriend(friendName: string){
    return this.client.post(`${environment.baseUrl}/api/friend/${friendName}`, undefined);
  }

  removeFriend(friendName: string){
    return this.client.delete(`${environment.baseUrl}/api/friend/${friendName}`);
  }
}
