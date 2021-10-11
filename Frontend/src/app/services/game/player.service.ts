import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private client: HttpClient) {}

  decrementPlayerHealth() : Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/player/decrement-health`, undefined);
  }
}
