import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  rewriteAccessToken(accessToken: string) {
    localStorage.setItem('accessToken', accessToken);
  }
  rewriteRefreshToken(refreshToken: string) {
    localStorage.setItem('refreshToken', refreshToken);
  }

  getAccessToken(): string {
    let token = localStorage.getItem('accessToken');
    return token ? token : "";
  }
  getRefreshToken(): string {
    let token = localStorage.getItem('refreshToken');
    return token ? token : "";
  }
  getUsername(): string  {
    let username = localStorage.getItem('username');
    return username ? username : "";
  }

  saveUser(accessToken: string, refreshToken: string, username: string) {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('username', username);
  }

  clear() {
    localStorage.clear();
  }
}
