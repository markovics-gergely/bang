import { Pipe, PipeTransform } from '@angular/core';
import { CardType } from '../models';

@Pipe({
  name: 'cardType'
})
export class CardTypePipe implements PipeTransform {
  private static lookup = ["Bang", "Missed", "Beer", "CatBalou", 
                           "Panic", "Duel", "GeneralStore", "Indians", 
                           "Stagecoach", "Gatling", "Saloon", "WellsFargo", 
                           "Jail", "Mustang", "Barrel", "Scope", 
                           "Dynamite", "Volcanic", "Schofield", "Remingtion", 
                           "Karabine", "Winchester"];
  transform(value: CardType): string {
    return CardTypePipe.lookup[value];
  }

}
