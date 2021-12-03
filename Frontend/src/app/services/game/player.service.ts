import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CharacterDto, OtherPlayer, Permissions, PlayCardDto } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private client: HttpClient) {}

  decrementPlayerHealth() : Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/player/decrement-health`, undefined);
  }

  getPermissions() : Observable<Permissions> {
    return this.client.get<Permissions>(`${environment.baseUrl}/api/bang/player/permissions`);
  }

  getTargetablesByRange(id: number, range: number) : Observable<OtherPlayer[]> {
    return this.client.get<OtherPlayer[]>(`${environment.baseUrl}/api/bang/player/${id}/targetable/${range}`);
  }

  getTargetables(id: number) : Observable<OtherPlayer[]> {
    return this.client.get<OtherPlayer[]>(`${environment.baseUrl}/api/bang/player/${id}/targetable`);
  }

  public useCharacter(characterDto: CharacterDto): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/player/use-character`, characterDto);
  }

  public discardFromHand(playerCardId: number): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/card/discard-card/${playerCardId}`, undefined);
  }

  public playFromHand(playCardDto: PlayCardDto): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/card/play-card`, playCardDto);
  }

  public endTurn(): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/gameboard/end-turn`, undefined);
  }

  public gainHealthForCards(cards: number[]): Observable<Object> {
    return this.client.put(`${environment.baseUrl}/api/bang/player/gain-health-for-cards`, cards);
  }
}
