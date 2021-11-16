import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HistoryViewModel } from 'src/app/models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  constructor(private client: HttpClient) { }

  getHistory(): Observable<HistoryViewModel[]>{
    return this.client.get<HistoryViewModel[]>(`${environment.baseUrl}/api/history`);
  }

  addHistory(history: HistoryViewModel){
    return this.client.post(`${environment.baseUrl}/api/history?playedRole=${history.playedRole}&placement=${history.placement}`, undefined);
  }
}
