import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { RegistrationDto } from 'src/app/models';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/services/authorization/token.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    confirmedPassword: new FormControl('', Validators.required)
  });

  constructor(private authorizationService: AuthorizationService, private router: Router/*, private snackbar: SnackbarService*/) { }

  ngOnInit(): void {}

  registration(){
    let registrationDto: RegistrationDto = { 
      username: this.registrationForm.get('username')?.value, 
      password: this.registrationForm.get('password')?.value,
      confirmedPassword: this.registrationForm.get('confirmedPassword')?.value,
    };

    
    this.authorizationService.registration(registrationDto).subscribe(
      res => {
        console.log(res)

        //this.snackbar.open("Registration is successful!")
        this.router.navigate(['login']);
      },
      err => {
        console.log(err)
      } 
    );  
  }
}
