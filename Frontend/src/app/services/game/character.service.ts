import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Character, CharacterType } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  private static assetPath: string = "../../../assets/cards/Characters/";

  constructor(private client: HttpClient) {
  }

  getCharacterPath(character: string) {
    return CharacterService.assetPath + character + ".png";
  }

  getCharacterByType(type: CharacterType): Observable<Character> {
    return this.client.get<Character>(`${environment.bangBaseUrl}/character/` + type);
  }
}
