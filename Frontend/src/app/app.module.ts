import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';

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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FriendComponent } from './pages/menu/friend/friend.component';
import { LobbyComponent } from './pages/menu/lobby/lobby.component';
import { ChatComponent } from './pages/chat/chat.component';
import { NgScrollbarModule } from 'ngx-scrollbar';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    FriendComponent,
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
    MenuComponent,
    LobbyComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgScrollbarModule,
    MatSnackBarModule
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
