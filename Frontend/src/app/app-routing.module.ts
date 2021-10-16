import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './pages/authorization/login/login.component';
import { GameboardComponent } from './pages/game/gameboard/gameboard.component';
import { RegistrationComponent } from './pages/authorization/registration/registration.component';
import { MenuComponent } from './pages/menu/menu/menu.component';
import { HistoryComponent } from './pages/menu/history/history.component';
import { LobbyComponent } from './pages/menu/lobby/lobby.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'registration',
    component: RegistrationComponent,
  },
  {
    path: 'menu',
    component: MenuComponent,
  },
  {
    path: 'history',
    component: HistoryComponent,
  },
  {
    path: 'lobby',
    component: LobbyComponent,
  },
  {
    path: 'gameboard',
    component: GameboardComponent,
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
