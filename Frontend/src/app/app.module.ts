import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { GameboardComponent } from './pages/gameboard/gameboard.component';
import { OwnboardComponent } from './pages/ownboard/ownboard.component';
import { OtherboardComponent } from './pages/otherboard/otherboard.component';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CardComponent } from './pages/card/card.component';
import { CardColorTypePipe } from './pipes/card-color-type.pipe';
import { CardTypePipe } from './pipes/card-type.pipe';
import { CardNumberPipe } from './pipes/card-number.pipe';
import { RoleTypePipe } from './pipes/role-type.pipe';
import { CharacterTypePipe } from './pipes/character-type.pipe';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    GameboardComponent,
    OwnboardComponent,
    OtherboardComponent,
    CardComponent,
    CardColorTypePipe,
    CardTypePipe,
    CardNumberPipe,
    RoleTypePipe,
    CharacterTypePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
