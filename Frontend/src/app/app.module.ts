import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/authorization/login/login.component';
import { RegistrationComponent } from './pages/authorization/registration/registration.component';
import { GameboardComponent } from './pages/game/gameboard/gameboard.component';
import { OwnboardComponent } from './pages/game/ownboard/ownboard.component';
import { OtherboardComponent } from './pages/game/otherboard/otherboard.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CardComponent } from './pages/game/card/card.component';
import { CardColorTypePipe } from './pipes/card-color-type.pipe';
import { CardTypePipe } from './pipes/card-type.pipe';
import { CardNumberPipe } from './pipes/card-number.pipe';
import { RoleTypePipe } from './pipes/role-type.pipe';
import { CharacterTypePipe } from './pipes/character-type.pipe';
import { CastPipe } from './pipes/cast.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MenuComponent } from './pages/menu/menu/menu.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';

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
    CharacterTypePipe,
    CastPipe,
    MenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
