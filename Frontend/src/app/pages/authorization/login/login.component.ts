import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginDto, LoginResponse } from 'src/app/models';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
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

  token: string = this.tokenService.getAccessToken();

  constructor(private authorizationService: AuthorizationService, private tokenService: TokenService, private router: Router/*, private snackbar: SnackbarService*/) {}

  ngOnInit(): void {}
  
  public login() {
    let loginDto: LoginDto = { 
      username: this.loginForm.get('username')?.value, 
      password: this.loginForm.get('password')?.value 
    };

    this.authorizationService.login(loginDto).subscribe(
      res => {
        console.log(res);
      
        //this.snackbar.open("Login is successful!");
        this.setTokenData(loginDto, res as LoginResponse);
        this.router.navigate(['menu']);
      },
      err => {
        console.log(err);

        //this.snackbar.open("Try again!");
        this.tokenService.clear();
      }
    );
  }

  private setTokenData(loginDto: LoginDto, res: LoginResponse){
    this.token = res.access_token;
    this.tokenService.saveUser(res.access_token, res.refresh_token, loginDto.username);
  }
}
