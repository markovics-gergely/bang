import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegistrationDto, LoginDto } from '../models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private static readonly grant_type: string = "password";
  private static readonly client_id: string = "useridentity";
  private static readonly client_secret: string = "ded22417709fa17aa4db549408d863e6ec6d44c25719fd5e64543b6eca843632";
  private static readonly scope: string = "useridentity";

  constructor(private client: HttpClient) { }

  public registration(registrationDto: RegistrationDto): Observable<Object> {
    return this.client.post(`${environment.baseUrl}/api/identity/registration`, registrationDto)
  }

  public login(loginDto: LoginDto): Observable<Object> {
    let headers = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
    let body = new URLSearchParams();

    body.set('username', loginDto.username);
    body.set('password', loginDto.password);
    body.set('grant_type', AuthorizationService.grant_type);
    body.set('client_id', AuthorizationService.client_id);
    body.set('client_secret', AuthorizationService.client_secret);

    return this.client.post(`${environment.baseUrl}/api/identity/login`, body.toString(), {headers: headers})
  }

  public getActualUserId(): Observable<Object> {
    return this.client.get(`${environment.baseUrl}/api/identity/actual-account`)
  }
}
