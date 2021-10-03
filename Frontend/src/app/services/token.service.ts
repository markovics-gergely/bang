import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  rewriteToken(token: string) {
    localStorage.setItem('userToken', token);
  }
  rewriteRefreshToken(refreshToken: string) {
    localStorage.setItem('refreshToken', refreshToken);
  }

  getToken(): string {
    let token = localStorage.getItem('userToken');
    return token ? token : "";
  }
  getRefreshToken(): string {
    let token = localStorage.getItem('refreshToken');
    return token ? token : "";
  }
  getUserName(): string  {
    let userName = localStorage.getItem('userName');
    return userName ? userName : "";
  }

  saveUser(token: string, refreshToken: string, username: string) {
    localStorage.setItem('userToken', token);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('userName', username);
  }

  clear() {
    localStorage.clear();
  }
}
