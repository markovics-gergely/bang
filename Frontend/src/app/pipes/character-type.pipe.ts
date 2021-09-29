import { Pipe, PipeTransform } from '@angular/core';
import { CharacterType } from '../models';

@Pipe({
  name: 'characterType'
})
export class CharacterTypePipe implements PipeTransform {
  private static lookup = ["BartCassidy", "BlackJack", "CalamityJanet", "ElGringo", 
                           "JesseJones", "Jourdonnais", "KitCarlson", "LuckyDuke", 
                           "PaulRegret", "PedroRamirez", "RoseDoolan", "SidKetchum", 
                           "SlabTheKiller", "SuzyLafayette", "VultureSam", "WillyTheKid", ];
  transform(value: CharacterType): string {
    return CharacterTypePipe.lookup[value];
  }

}
