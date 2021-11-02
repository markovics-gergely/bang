import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HubBuilderService {

  

  getConnection() {
    return new signalR.HubConnectionBuilder()
      .withUrl(`${environment.userIdentityBaseUrl}/chathub`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
    /*.withUrl("/lobbyhub")
      .withUrl("/friendhub")*/
    .configureLogging(signalR.LogLevel.Information)
    .build();
  } 
}
