import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  token: string = this.getAccessToken();

  getAccessToken(): string {
    let token = localStorage.getItem('accessToken');
    return token ? token : "";
  }

  getRefreshToken(): string {
    let token = localStorage.getItem('refreshToken');
    return token ? token : "";
  }

  setAccessToken(accessToken: string) {
    localStorage.setItem('accessToken', accessToken);
  }

  setRefreshToken(refreshToken: string) {
    localStorage.setItem('refreshToken', refreshToken);
  }

  getUsername(): string  {
    let username = localStorage.getItem('username');
    return username ? username : "";
  }

  setLocalStorage(accessToken: string, refreshToken: string, username: string) {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('username', username);
  }

  deleteLocalStorage() {
    localStorage.clear();
  }
}
