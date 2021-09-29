import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  private static assetPath: string = "../../assets/cards/Characters/";

  getCharacterPath(character: string) {
    return CharacterService.assetPath + character + ".png";
  }
}
