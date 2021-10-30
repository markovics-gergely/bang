import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginDto, LoginResponse } from 'src/app/models';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription, timer } from 'rxjs';
import { TokenService } from 'src/app/services/authorization/token.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  constructor(
    private authService: AuthorizationService, 
    private tokenService: TokenService, 
    private snackbar: SnackbarService,
    private router: Router  
  ) {}

  ngOnInit(): void {
    this.tokenService.deleteLocalStorage();
  }
  
  public login() {
    let loginDto: LoginDto = { 
      username: this.loginForm.get('username')?.value, 
      password: this.loginForm.get('password')?.value 
    };

    this.authService.login(loginDto).subscribe(
      response => {
        console.log(response);
      
        this.snackbar.open("Login is successful!");
        this.setLocalStorage(loginDto, response as LoginResponse);
        this.router.navigate(['menu']);
      },
      error => {
        console.log(error);

        this.snackbar.open("Wrong username or password!");
        this.tokenService.deleteLocalStorage();
      }
    );
  }

  private setLocalStorage(loginDto: LoginDto, res: LoginResponse){
    this.tokenService.token = res.access_token;
    this.tokenService.setLocalStorage(res.access_token, res.refresh_token, loginDto.username);

    const refreshTimer = timer((res.expires_in*1000)-5000, (res.expires_in*1000)-5000);    
    refreshTimer.subscribe(() => {
      this.authService.refresh(this.tokenService.getRefreshToken()).subscribe(
        res => {
          console.log(res)

          let response = res as LoginResponse;
          this.tokenService.token = response.access_token;
          this.tokenService.setAccessToken(response.access_token)
          this.tokenService.setRefreshToken(response.refresh_token);      
        }, 
        err => {
          console.log(err);

          this.tokenService.deleteLocalStorage();
        }
      )
    })
  }
}
